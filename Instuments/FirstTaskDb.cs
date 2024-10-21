using System.Data;
using System.Data.SqlClient;

namespace Task1.Instuments
{
    //Статический класс, отвечающий за взаимодействие с базой данных
    public class FirstTaskDb
    {
        //Таблица назначения данных
        static string Table = "Task1";
        //Строка подключения к БД
        static string dbConnenctionString = "Data Source=DESKTOP-93G6P48\\USEFUL;Initial Catalog=B1DIGITAL;Integrated Security=True;Encrypt=False;Connection Timeout=120;;Pooling=false";
        public FirstTaskDb()
        {

        }
        public static int ImportDb(List<string[]> lines)
        {//Импрот строк из 1 файла в БД 
            try
            {
                using (var conn = new SqlConnection(dbConnenctionString))
                {//Открытие соединения с БД Sql Server
                    conn.Open();
                    //Создание транзакции
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Создание DataTable для вставки данных
                            DataTable dataTable = new DataTable();
                            //Определение структуры dataTable
                            dataTable.Columns.Add("TaskDate", typeof(DateTime));
                            dataTable.Columns.Add("TaskEngSymbols", typeof(string));
                            dataTable.Columns.Add("TaskRusSymbols", typeof(string));
                            dataTable.Columns.Add("TaskInt", typeof(int));
                            dataTable.Columns.Add("TaskFloat", typeof(float));

                            // Заполнение DataTable данными
                            foreach (var line in lines)
                            {//Заполнение происходит построчно
                                DataRow row = dataTable.NewRow();
                                row["TaskDate"] = DateTime.Parse(line[0]);
                                row["TaskEngSymbols"] = line[1];
                                row["TaskRusSymbols"] = line[2];
                                row["TaskInt"] = int.Parse(line[3]);
                                row["TaskFloat"] = Math.Round(float.Parse(line[4]),8);
                                dataTable.Rows.Add(row);
                            }
                            // Использование SqlBulkCopy для массовой вставки данных
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                            {
                                //Установка таблицы для загрузки
                                bulkCopy.DestinationTableName = Table;
                                // Указание соответствий столбцов между DataTable и таблицей базы данных
                                bulkCopy.ColumnMappings.Add("TaskDate", "TaskDate");
                                bulkCopy.ColumnMappings.Add("TaskEngSymbols", "TaskEngSymbols");
                                bulkCopy.ColumnMappings.Add("TaskRusSymbols", "TaskRusSymbols");
                                bulkCopy.ColumnMappings.Add("TaskInt", "TaskInt");
                                bulkCopy.ColumnMappings.Add("TaskFloat", "TaskFloat");
                                //Установка ограничения количества действий в 1 запросе. При большем количестве создаётся новый запрос с таким же ограничением 
                                bulkCopy.BatchSize = 5000;
                                //Запись в бд
                                bulkCopy.WriteToServer(dataTable);
                            }
                            //Подтверждение действий транзакций, освобождение ресурсов
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            //Откат всех сделанных до ошибки операций, освобождение ресурсов
                            transaction.Rollback();
                            throw;
                        }
                    }
                    //закрытие подключения к бд
                    conn.Close();
                    //Если данные занесены успешно, то перенесено всё, что было в файле. Потому возвращается количество строк файла
                    return lines.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        public static string[] CalculateSumAndMedian(string procedureName)
        {//Вызов хранимой процедуры БД по расчёту суммы целых чисел и медианы дробных чисел
            //создание массива для результатов
            string[] values = new string[2];
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnenctionString))
                {//открытие соединения с БД
                    conn.Open();
                    //текст запроса вызова процедуры
                    string text = $"exec {procedureName}; go";
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandTimeout = 240;
                        command.CommandText = text;
                        //выполнение запроса, получение объекта чтения результатов запроса
                        var reader = command.ExecuteReader();
                        //проверка на наличие строки значений
                        if (reader.Read())
                        {//запись результатов в массив
                            values[0] = reader[0].ToString();
                            values[1] = reader[1].ToString();
                        }
                    }
                    //разрыв соединения с БД
                    conn.Close();
                    return values;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
