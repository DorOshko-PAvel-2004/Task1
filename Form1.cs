using Task1.Instuments;

namespace Task1
{
    public partial class Form1 : Form
    {
        //Два данных поля используются в программе как папки хранения объединённого файла и генерации 100 файлов соответсвенно
        string joinPath = "joined";
        string filesPath = "";
        //название папки хранения процедуры для выполнения задания 4. Название файла - название процедуры 
        string procedurePath = "procedures";
        //Поток, использованный для передачи выполнения 1 из задач в отдельный поток
        Thread proccess;
        public Form1()
        {
            InitializeComponent();
        }
        
        private async void buttonFiles_ClickAsync(object sender, EventArgs e)
        {//Генерация файлов

            //если выполняется на данный момент загрузка данных в БД или генерация файлов, функция завершает работу
            if (!IsProcessing()) { return; }
            try
            {
                Random random = new Random();//создан объект для генерации данных
                int lines = 100000;//количесвто генерируемых строк в 1 файле по условию
                int files = 100;//количество генерируемых файлов по условию
                //вызов окна выбора папки для хранения генерируемых файлов
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                //путь к выбранной папке
                filesPath = folderBrowserDialog1.SelectedPath;
                //формируется отдельный поток для выполнения задачи
                proccess = new Thread(() =>
                {
                    for (int f = 1; f <= files; f++)
                    {//название файла состоит из его номера. Каждый файл создаётся на 1 обходе цикла.
                     //Если такой файл уже существует, он перезаписывается (свойство false). Кодировка UTF-8 используется для записи русских символов
                        using (StreamWriter writer = new StreamWriter($"{filesPath}//{f}.txt", false, System.Text.Encoding.UTF8))
                        {//открыт поток записи в создаваемый файл
                            for (int i = 0; i < lines; i++)
                            {//генерация значений
                                DateTime date = FirstTaskRandom.ReturnRandomDate(random);
                                string latArray = FirstTaskRandom.ReturnRandomEngString(random);
                                string rusArray = FirstTaskRandom.ReturnRandomRusString(random);
                                int randomInt = FirstTaskRandom.ReturnRandomInt(random);
                                double randomDouble = FirstTaskRandom.ReturnRandomDouble(random);
                                //объединение значений в 1 строку по данному условием шаблону, готовой для записи в файл
                                string line = string.Join("||", date.ToString("dd.MM.yyyy"), latArray, rusArray, randomInt, randomDouble);
                                writer.WriteLine(line);//запись в файл
                            }
                        }
                    }
                    //сообщение пользователю об успешном окончании генерации
                MessageBox.Show($"All files are inloaded!");
                });
                proccess.Name = "Files' generation";
                proccess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UniteButton_Click(object sender, EventArgs e)
        {//Объединение файлов в 1
            //если выполняется на данный момент загрузка данных в БД или генерация файлов, функция завершает работу
            if (!IsProcessing())
            { return; }
            try
            {
                //создание полного пути нового файла. Папка хранения указана в поле joinPath
                string joinFilePath = ReturnJoinFileName(joinPath);
                //проверка на пустое значение полного пути нового файла
                if (joinFilePath == "") { return; }
                MessageBox.Show("Select folder of files.");
                //выбор папки хранения сгенерируемых файлов и проверка на подтверждение выбора
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Files' folder is not chosen. Operation is finished.");
                    return;
                }
                //получение пути к папке сгенерируемых файлов filesPath
                filesPath = folderBrowserDialog1.SelectedPath;
                //получение списка текстовых файлов из папки filesPath
                List<string> files = new List<string>(Directory.GetFiles(filesPath).Where(x=>x.Contains(".txt")).ToList());
                //Создание объекта записи в файл по пути joinFilePath
                using (StreamWriter writer = new StreamWriter(joinFilePath, false, System.Text.Encoding.UTF8))
                {
                    //настройка буфера. После каждого действия, связанного с потоком записи в файл, очищать буфер
                    writer.AutoFlush = true;
                    //Цикл просмотра каждого из текстовых файлов папки filesPath
                    foreach (var file in files)
                    {
                        //чтение всех строк из файла с папки filesPath
                        string[] lines = System.IO.File.ReadAllLines(file);
                        foreach (var line in lines)
                        {//Запись каждой строки из файла в объединённый файл
                            writer.WriteLine(line);
                        }
                    }
                }
                //Сообщение об успешном создании объединённого файла
                MessageBox.Show("File is created.");
            }
            catch (Exception ex)
            {
                //Сообщение об ошибке
                MessageBox.Show(ex.Message);
            }
        }

        //Возвращение полного пути к создаваемому объединённому файлу
        //В качестве параметра передаётся путь папки, где будет хранится файл
        private string ReturnJoinFileName(string path)
        {
            //Вызов формы и получения названия файла. Желательный формат: txt, прописывается вручную
            string joinFilePath = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of joined file:", "Creation of joined file").Trim();

            // Проверка на ввод имени папки на случай, если пользователь не заполнил форму
            if (joinFilePath != "")
            {
                // Составляение полного пути к новому файлу через соединение пути к папке и названия файла
                joinFilePath = Path.Combine(path, joinFilePath);

                if (System.IO.File.Exists(joinFilePath))
                {//Если такой файл уже существует, то запрашивается перезапись файла
                    DialogResult dialogResult = MessageBox.Show("File exists with same path.\nDo you want rewriting this?", "Rewrite", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {//если соглашение получено, старый файл удаляется
                        System.IO.File.Delete(joinFilePath);
                        MessageBox.Show("File will be updated.");
                    }
                    else
                    {
                        MessageBox.Show("File isn't created.");
                        return "";
                    }
                }
            }
            return joinFilePath;
        }

        private void buttonClearFiles_Click(object sender, EventArgs e)
        {//Удаление из файлов строк, содержащих комбинацию символов, указанная в поле BoxDeletingRow
         //Выполняется только при наличии объединённого файла
            //если выполняется на данный момент загрузка данных в БД или генерация файлов, функция завершает работу
            if (!IsProcessing())
            {
                return;
            }
            try
            {
                //Проверка на отсутствие объединённых файлов 
                if (Directory.GetFiles(joinPath).Count() == 0)
                {
                    MessageBox.Show("Before clearing you have to have a joined file");
                    return;
                }
                //Получение комбинации символов из текстового поля формы BoxDeletingRow
                string combination = BoxDeletingRow.Text;
                //Счётчик удалённых строк по всем файлам
                int countClear = 0;
                //выбор папки сгенерируеммых файлов и проверка подтверждения выбора
                if(folderBrowserDialog1.ShowDialog()!=DialogResult.OK)
                {
                    MessageBox.Show("Files' folder is not chosen. Method is finished.");
                    return;
                }
                filesPath = folderBrowserDialog1.SelectedPath;
                //получение списка путей файлов выбранной папки
                var files = Directory.GetFiles(filesPath);
                foreach (var file in files)
                {
                    //список всех строк с файла, путь которого равен file 
                    List<string> lines = System.IO.File.ReadAllLines(file).ToList();
                    //список строк, не включающие в себя комбинацию combination
                    List<string> newLines = lines.Where(x => !x.Contains(combination)).ToList();
                    //счетчик пополняется на разность между количесвтом всех строк и строк, не включающих в себя combination
                    countClear += lines.Count - newLines.Count;
                    //создание потока для перезаписи файла с путём file
                    using (StreamWriter writer = new StreamWriter(file, false, System.Text.Encoding.UTF8))
                    {
                        foreach (var line in newLines)
                        {//запись строки в файл
                            writer.WriteLine(line);
                        }
                    }
                }
                //Сообщение о количестве удалённых строк
                MessageBox.Show($"{countClear} rows were deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonDbMove_ClickAsync(object sender, EventArgs e)
        {//Загрузка сгенерируемых строк из файлов в баззу данных
            //если выполняется на данный момент загрузка данных в БД или генерация файлов, функция завершает работу
            if (!IsProcessing())
            {
                return;
            }
            try
            {
                MessageBox.Show("Select folder of files.");
                //выбор папки хранения сгенерируемых файлов и проверка на подтверждение выбора
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Files' folder is not chosen. Operation is finished.");
                    return;
                }
                filesPath = folderBrowserDialog1.SelectedPath;
                //получение файлов из выбранной папки
                string[] files = Directory.GetFiles(filesPath);
                //счётчик загруженных строк
                int linesLoaded = 0;
                TextDbLoaded.Text = $"Await...";
                //Создание потока для записи в базу данных
                proccess = new Thread(() =>
                {
                    foreach (var file in files)
                    {//чтение происходит по каждому файлу по отдельности
                        //деление строки на элементы, разделённые символом ||, получение списка массивов элементов
                        var lines = System.IO.File.ReadAllLines(file).Select(x => x.Split("||").ToArray()).ToList();
                        //вызов метода импорта в БД, получение количества загруженных файлов
                        linesLoaded += FirstTaskDb.ImportDb(lines);
                        //Изменение текста формы, вывод текущего количества загруженных строк на форму 
                        this.Invoke((MethodInvoker)delegate { TextDbLoaded.Text = $"Await...\n{linesLoaded} lines are loaded in database."; });
                    }
                    //вывод итогового количества загруженных строк
                    this.Invoke((MethodInvoker)delegate { TextDbLoaded.Text = $"{linesLoaded} lines are loaded in database."; });
                });
                proccess.Name = "Loading in database";
                //запуск потока
                proccess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCalculSumAndMedian_Click(object sender, EventArgs e)
        {//Подсчёт и вывод на форму суммы целых значений и медианы среди дробных чисел
            
            //если выполняется на данный момент загрузка данных в БД или генерация файлов, функция завершает работу
            if (!IsProcessing())
            {
                return;
            }
            try
            {
                //поиск названия процедуры в папке procedurePath для выполнения запроса
                string procedureName = Directory.GetFiles(procedurePath).Single(x => x.Contains("SumAndMedian")).Split("\\").Last().Split(".sql").First();
                //вызов метода, возращающий массив из 2 элементов: суммы и медианы
                string[] values = FirstTaskDb.CalculateSumAndMedian(procedureName);
                if (values == null) { throw new NullReferenceException(); }
                //вывод полученных значений на форму
                labelSum.Text = values[0];
                labelMedian.Text = values[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool IsProcessing()
        {//проверка на статус процесса загрузки данных в БД. Если процесс не закночен, то выводится сообщение и возвращается false
            //иначе - возвращается true
            if (proccess?.ThreadState == System.Threading.ThreadState.Running)
            {
                MessageBox.Show($"Canceled.\n{proccess?.Name} is executing");
                return false;
            }
            return true;
        }

    }

}
