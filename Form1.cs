using Task1.Instuments;

namespace Task1
{
    public partial class Form1 : Form
    {
        //��� ������ ���� ������������ � ��������� ��� ����� �������� ������������ ����� � ��������� 100 ������ �������������
        string joinPath = "joined";
        string filesPath = "";
        //�������� ����� �������� ��������� ��� ���������� ������� 4. �������� ����� - �������� ��������� 
        string procedurePath = "procedures";
        //�����, �������������� ��� �������� ���������� 1 �� ����� � ��������� �����
        Thread proccess;
        public Form1()
        {
            InitializeComponent();
        }
        
        private async void buttonFiles_ClickAsync(object sender, EventArgs e)
        {//��������� ������

            //���� ����������� �� ������ ������ �������� ������ � �� ��� ��������� ������, ������� ��������� ������
            if (!IsProcessing()) { return; }
            try
            {
                Random random = new Random();//������ ������ ��� ��������� ������
                int lines = 100000;//���������� ������������ ����� � 1 ����� �� �������
                int files = 100;//���������� ������������ ������ �� �������
                //����� ���� ������ ����� ��� �������� ������������ ������
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                //���� � ��������� �����
                filesPath = folderBrowserDialog1.SelectedPath;
                //����������� ��������� ����� ��� ���������� ������
                proccess = new Thread(() =>
                {
                    for (int f = 1; f <= files; f++)
                    {//�������� ����� ������� �� ��� ������. ������ ���� �������� �� 1 ������ �����.
                     //���� ����� ���� ��� ����������, �� ���������������� (�������� false). ��������� UTF-8 ������������ ��� ������ ������� ��������
                        using (StreamWriter writer = new StreamWriter($"{filesPath}//{f}.txt", false, System.Text.Encoding.UTF8))
                        {//������ ����� ������ � ����������� ����
                            for (int i = 0; i < lines; i++)
                            {//��������� ��������
                                DateTime date = FirstTaskRandom.ReturnRandomDate(random);
                                string latArray = FirstTaskRandom.ReturnRandomEngString(random);
                                string rusArray = FirstTaskRandom.ReturnRandomRusString(random);
                                int randomInt = FirstTaskRandom.ReturnRandomInt(random);
                                double randomDouble = FirstTaskRandom.ReturnRandomDouble(random);
                                //����������� �������� � 1 ������ �� ������� �������� �������, ������� ��� ������ � ����
                                string line = string.Join("||", date.ToString("dd.MM.yyyy"), latArray, rusArray, randomInt, randomDouble);
                                writer.WriteLine(line);//������ � ����
                            }
                        }
                    }
                    //��������� ������������ �� �������� ��������� ���������
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
        {//����������� ������ � 1
            //���� ����������� �� ������ ������ �������� ������ � �� ��� ��������� ������, ������� ��������� ������
            if (!IsProcessing())
            { return; }
            try
            {
                //�������� ������� ���� ������ �����. ����� �������� ������� � ���� joinPath
                string joinFilePath = ReturnJoinFileName(joinPath);
                //�������� �� ������ �������� ������� ���� ������ �����
                if (joinFilePath == "") { return; }
                MessageBox.Show("Select folder of files.");
                //����� ����� �������� ������������� ������ � �������� �� ������������� ������
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Files' folder is not chosen. Operation is finished.");
                    return;
                }
                //��������� ���� � ����� ������������� ������ filesPath
                filesPath = folderBrowserDialog1.SelectedPath;
                //��������� ������ ��������� ������ �� ����� filesPath
                List<string> files = new List<string>(Directory.GetFiles(filesPath).Where(x=>x.Contains(".txt")).ToList());
                //�������� ������� ������ � ���� �� ���� joinFilePath
                using (StreamWriter writer = new StreamWriter(joinFilePath, false, System.Text.Encoding.UTF8))
                {
                    //��������� ������. ����� ������� ��������, ���������� � ������� ������ � ����, ������� �����
                    writer.AutoFlush = true;
                    //���� ��������� ������� �� ��������� ������ ����� filesPath
                    foreach (var file in files)
                    {
                        //������ ���� ����� �� ����� � ����� filesPath
                        string[] lines = System.IO.File.ReadAllLines(file);
                        foreach (var line in lines)
                        {//������ ������ ������ �� ����� � ����������� ����
                            writer.WriteLine(line);
                        }
                    }
                }
                //��������� �� �������� �������� ������������ �����
                MessageBox.Show("File is created.");
            }
            catch (Exception ex)
            {
                //��������� �� ������
                MessageBox.Show(ex.Message);
            }
        }

        //����������� ������� ���� � ������������ ������������ �����
        //� �������� ��������� ��������� ���� �����, ��� ����� �������� ����
        private string ReturnJoinFileName(string path)
        {
            //����� ����� � ��������� �������� �����. ����������� ������: txt, ������������� �������
            string joinFilePath = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of joined file:", "Creation of joined file").Trim();

            // �������� �� ���� ����� ����� �� ������, ���� ������������ �� �������� �����
            if (joinFilePath != "")
            {
                // ������������ ������� ���� � ������ ����� ����� ���������� ���� � ����� � �������� �����
                joinFilePath = Path.Combine(path, joinFilePath);

                if (System.IO.File.Exists(joinFilePath))
                {//���� ����� ���� ��� ����������, �� ������������� ���������� �����
                    DialogResult dialogResult = MessageBox.Show("File exists with same path.\nDo you want rewriting this?", "Rewrite", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {//���� ���������� ��������, ������ ���� ���������
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
        {//�������� �� ������ �����, ���������� ���������� ��������, ��������� � ���� BoxDeletingRow
         //����������� ������ ��� ������� ������������ �����
            //���� ����������� �� ������ ������ �������� ������ � �� ��� ��������� ������, ������� ��������� ������
            if (!IsProcessing())
            {
                return;
            }
            try
            {
                //�������� �� ���������� ����������� ������ 
                if (Directory.GetFiles(joinPath).Count() == 0)
                {
                    MessageBox.Show("Before clearing you have to have a joined file");
                    return;
                }
                //��������� ���������� �������� �� ���������� ���� ����� BoxDeletingRow
                string combination = BoxDeletingRow.Text;
                //������� �������� ����� �� ���� ������
                int countClear = 0;
                //����� ����� �������������� ������ � �������� ������������� ������
                if(folderBrowserDialog1.ShowDialog()!=DialogResult.OK)
                {
                    MessageBox.Show("Files' folder is not chosen. Method is finished.");
                    return;
                }
                filesPath = folderBrowserDialog1.SelectedPath;
                //��������� ������ ����� ������ ��������� �����
                var files = Directory.GetFiles(filesPath);
                foreach (var file in files)
                {
                    //������ ���� ����� � �����, ���� �������� ����� file 
                    List<string> lines = System.IO.File.ReadAllLines(file).ToList();
                    //������ �����, �� ���������� � ���� ���������� combination
                    List<string> newLines = lines.Where(x => !x.Contains(combination)).ToList();
                    //������� ����������� �� �������� ����� ����������� ���� ����� � �����, �� ���������� � ���� combination
                    countClear += lines.Count - newLines.Count;
                    //�������� ������ ��� ���������� ����� � ���� file
                    using (StreamWriter writer = new StreamWriter(file, false, System.Text.Encoding.UTF8))
                    {
                        foreach (var line in newLines)
                        {//������ ������ � ����
                            writer.WriteLine(line);
                        }
                    }
                }
                //��������� � ���������� �������� �����
                MessageBox.Show($"{countClear} rows were deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonDbMove_ClickAsync(object sender, EventArgs e)
        {//�������� ������������� ����� �� ������ � ����� ������
            //���� ����������� �� ������ ������ �������� ������ � �� ��� ��������� ������, ������� ��������� ������
            if (!IsProcessing())
            {
                return;
            }
            try
            {
                MessageBox.Show("Select folder of files.");
                //����� ����� �������� ������������� ������ � �������� �� ������������� ������
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Files' folder is not chosen. Operation is finished.");
                    return;
                }
                filesPath = folderBrowserDialog1.SelectedPath;
                //��������� ������ �� ��������� �����
                string[] files = Directory.GetFiles(filesPath);
                //������� ����������� �����
                int linesLoaded = 0;
                TextDbLoaded.Text = $"Await...";
                //�������� ������ ��� ������ � ���� ������
                proccess = new Thread(() =>
                {
                    foreach (var file in files)
                    {//������ ���������� �� ������� ����� �� �����������
                        //������� ������ �� ��������, ���������� �������� ||, ��������� ������ �������� ���������
                        var lines = System.IO.File.ReadAllLines(file).Select(x => x.Split("||").ToArray()).ToList();
                        //����� ������ ������� � ��, ��������� ���������� ����������� ������
                        linesLoaded += FirstTaskDb.ImportDb(lines);
                        //��������� ������ �����, ����� �������� ���������� ����������� ����� �� ����� 
                        this.Invoke((MethodInvoker)delegate { TextDbLoaded.Text = $"Await...\n{linesLoaded} lines are loaded in database."; });
                    }
                    //����� ��������� ���������� ����������� �����
                    this.Invoke((MethodInvoker)delegate { TextDbLoaded.Text = $"{linesLoaded} lines are loaded in database."; });
                });
                proccess.Name = "Loading in database";
                //������ ������
                proccess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCalculSumAndMedian_Click(object sender, EventArgs e)
        {//������� � ����� �� ����� ����� ����� �������� � ������� ����� ������� �����
            
            //���� ����������� �� ������ ������ �������� ������ � �� ��� ��������� ������, ������� ��������� ������
            if (!IsProcessing())
            {
                return;
            }
            try
            {
                //����� �������� ��������� � ����� procedurePath ��� ���������� �������
                string procedureName = Directory.GetFiles(procedurePath).Single(x => x.Contains("SumAndMedian")).Split("\\").Last().Split(".sql").First();
                //����� ������, ����������� ������ �� 2 ���������: ����� � �������
                string[] values = FirstTaskDb.CalculateSumAndMedian(procedureName);
                if (values == null) { throw new NullReferenceException(); }
                //����� ���������� �������� �� �����
                labelSum.Text = values[0];
                labelMedian.Text = values[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool IsProcessing()
        {//�������� �� ������ �������� �������� ������ � ��. ���� ������� �� ��������, �� ��������� ��������� � ������������ false
            //����� - ������������ true
            if (proccess?.ThreadState == System.Threading.ThreadState.Running)
            {
                MessageBox.Show($"Canceled.\n{proccess?.Name} is executing");
                return false;
            }
            return true;
        }

    }

}
