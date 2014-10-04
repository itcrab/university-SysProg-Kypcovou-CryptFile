/*
 * Codename: МАСТЕР шифрования файлов 1.0
 * Developer: Arcady Usov aka ARC_Programmer
 * Date: 24.04.2008
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// crypt
using System.Security.Cryptography;
using System.IO;


namespace SysProg_Kypcovou_CryptFile
{
    public partial class mainForm : Form
    {
        // сообщения приложения
        String welcomeMessage = "Вы попали в МАСТЕР шифрования файлов, который поможет вам зашифровать секретные файлы используя пароль доступа, а так же расшифровать их при вводе верного пароля.";
        String tipRaboti = "Тип работы указывает мастеру, что он должен делать: шифровать или дешифровать выбранный файл.";
        String algoritm = "Алгоритм указывает мастеру шифрования файлов каким алгоритмом шифровать/дешифровать выбранный файл.";
        String putKFailu = "Путь к файлу указывает путь до выбранного файла.";
        String parolDostupa = "Пароль доступа указывает пароль доступа к выходному файлу.";
        String putVihod = "Путь к выходнму файлу указывает путь до выходного файла.";
        String log = "Это лог работы мастера шифрования файлов.";
        String processBar = "Индикатор хода работы мастера шифрования файлов.";


        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            // строим форму
            workPanel.Top = 27;
            workPanel.Left = 12;

            progressPanel.Top = 27;
            progressPanel.Left = 12;

            mainForm.ActiveForm.Width = 435;
            mainForm.ActiveForm.Height = 410;

            label2.Text = welcomeMessage;
            label3.Text = welcomeMessage;
            label5.Text = welcomeMessage;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            welcomePanel.Hide();
            progressBar1.Value = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели путь к выбираемому файлу!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели пароль доступа к выбранному файлу!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox3.Text.Trim() == "")
            {
                DialogResult question = MessageBox.Show("Вы не ввели имя выходного файла!\nФайл будет иметь стандартное имя!", "Ошибка!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (question != DialogResult.OK)
                {
                    return;
                }
                
            }

            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("Выбранного вами файла в поле \"Путь к файлу\" не существует или не возможен доступ к нему!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox3.Text.Trim()=="" & comboBox1.Text == "Шифрование" & File.Exists(textBox1.Text + "." + comboBox2.Text))
            {
                MessageBox.Show("Выходной файл не задан. Применение стандартного имени файла не возможно,\nт.к. файл с таким именем уже существует.\nЕсли у вас возникли вопросы - обратитесь к справке.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox3.Text.Trim() == "" & comboBox1.Text == "Дешифрование" & File.Exists(textBox1.Text + "." + comboBox2.Text + "none"))
            {
                MessageBox.Show("Выходной файл не задан. Применение стандартного имени файла не возможно,\nт.к. файл с таким именем уже существует.\nЕсли у вас возникли вопросы - обратитесь к справке.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            workPanel.Hide();

            if (textBox2.Text.Length > 0) textBox2.Text = textBox2.Text + "Ugm>9*#%";
            if (textBox2.Text.Length > 8) textBox2.Text = textBox2.Text.Remove(8);
            //label4.Text = textBox2.Text + textBox2.Text.Length;


            //System.Threading.Thread.Sleep(1000);
            if (comboBox1.Text.Equals("Шифрование"))
            {
                // шифруем
                if (comboBox2.Text == "DES")
                {

                    Encrypt_DES(textBox1.Text, textBox3.Text, textBox2.Text);
                }
                if (comboBox2.Text == "TripleDES")
                {
                    Encrypt_TripleDES(textBox1.Text, textBox3.Text, textBox2.Text);
                }
                if (comboBox2.Text == "RC2")
                {
                    Encrypt_RC2(textBox1.Text, textBox3.Text, textBox2.Text);
                }
                if (comboBox2.Text == "Rijndael (AES)")
                {
                    Encrypt_Rijndael(textBox1.Text, textBox3.Text, textBox2.Text);
                }
               


            }

            if (comboBox1.Text.Equals("Дешифрование"))
            {
                // дешифруем
                if (comboBox2.Text == "DES")
                {
                    Decrypt_DES(textBox1.Text, textBox3.Text, textBox2.Text);
                }
                if (comboBox2.Text == "TripleDES")
                {
                    Decrypt_TripleDES(textBox1.Text, textBox3.Text, textBox2.Text);
                }
                if (comboBox2.Text == "RC2")
                {
                    Decrypt_RC2(textBox1.Text, textBox3.Text, textBox2.Text);
                }
                if (comboBox2.Text == "Rijndael (AES)")
                {
                    Decrypt_Rijndael(textBox1.Text, textBox3.Text, textBox2.Text);
                }
                
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // заглушка
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "openFileDialog1")
            {
                
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            textBox3.Text = saveFileDialog1.FileName;

        }


        // DES
        private void Encrypt_DES(String fName, String fNameCrypt, String passWord)
        {
            Log_Message_Encrypt(0);
            String fEncrypt_DES = fName + ".DES";

            if (fNameCrypt.Trim() != "")
            {
                fEncrypt_DES = fNameCrypt;
            }

            try
            {
                SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("DES");
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(1);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes(passWord); //8
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(passWord); //8
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(2);
                FileStream outStream = File.OpenWrite(fEncrypt_DES);
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(3);
                ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(outStream, transform, CryptoStreamMode.Write);
                Byte[] fNameRead = File.ReadAllBytes(fName);
                this.progressPanel.Refresh();
                
                cryptoStream.Write(fNameRead, 0, fNameRead.Length);
                cryptoStream.Close();
                this.progressPanel.Refresh();
                Log_Message_Encrypt(4);
            }
            catch
            {
                MessageBox.Show("Ошибка шифрования! Пароль доступа не верный!","Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 3;
        }


        private void Decrypt_DES(String fName, String fNameCrypt, String passWord)
        {
            Log_Message_Decrypt(0);
            String decryptedPath = fName + ".DESnone";
            if (fNameCrypt.Trim() != "")
            {
                decryptedPath = fNameCrypt;
            }

            try
            {
                SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("DES");
                this.progressPanel.Refresh();
                
                Log_Message_Decrypt(1);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes(passWord); //8
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(passWord); //8
                this.progressPanel.Refresh();
                
                Log_Message_Decrypt(2);
                Log_Message_Decrypt(3);
                ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor();
                this.progressPanel.Refresh();
                
                Stream inStream = File.OpenRead(fName);
                CryptoStream cryptoStream = new CryptoStream(inStream, transform, CryptoStreamMode.Read);
                Byte[] buffer = new Byte[50];
                this.progressPanel.Refresh();
                
                int length = cryptoStream.Read(buffer, 0, buffer.Length);
                Stream outStream = File.OpenWrite(decryptedPath);
                this.progressPanel.Refresh();
                
                while (length > 0)
                {
                    outStream.Write(buffer, 0, length);
                    length = cryptoStream.Read(buffer, 0, buffer.Length);

                }
                this.progressPanel.Refresh();
                
                inStream.Close();
                outStream.Close();
                Log_Message_Decrypt(4);
            }
            catch
            {
                MessageBox.Show("Ошибка дешифрования!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 3;
        }



        // RC2
        private void Encrypt_RC2(String fName, String fNameCrypt, String passWord)
        {
            Log_Message_Encrypt(0);
            String fEncrypt_RC2 = fName + ".RC2";
            if (fNameCrypt.Trim() != "")
            {
                fEncrypt_RC2 = fNameCrypt;
            }

            try
            {
                SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("RC2");
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(1);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes(passWord); //8
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(passWord); //8
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(2);
                FileStream outStream = File.OpenWrite(fEncrypt_RC2);
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(3);
                ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(outStream, transform, CryptoStreamMode.Write);
                Byte[] fNameRead = File.ReadAllBytes(fName);
                cryptoStream.Write(fNameRead, 0, fNameRead.Length);
                this.progressPanel.Refresh();
                
                cryptoStream.Close();
                Log_Message_Encrypt(4);
            }
            catch
            {
                MessageBox.Show("Ошибка шифрования! Пароль доступа не верный!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 3;
        }


        private void Decrypt_RC2(String fName, String fNameCrypt, String passWord)
        {
            Log_Message_Decrypt(0);
            String decryptedPath = fName + ".RC2none";
            if (fNameCrypt.Trim() != "")
            {
                decryptedPath = fNameCrypt;
            }

            try
            {
                SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("RC2");
                this.progressPanel.Refresh();
                
                Log_Message_Decrypt(1);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes(passWord); //8
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(passWord); //8
                this.progressPanel.Refresh();
                
                Log_Message_Decrypt(2);
                Log_Message_Decrypt(3);
                ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor();
                this.progressPanel.Refresh();
                
                Stream inStream = File.OpenRead(fName);
                CryptoStream cryptoStream = new CryptoStream(inStream, transform, CryptoStreamMode.Read);
                this.progressPanel.Refresh();
                
                Byte[] buffer = new Byte[50];
                int length = cryptoStream.Read(buffer, 0, buffer.Length);
                Stream outStream = File.OpenWrite(decryptedPath);
                while (length > 0)
                {
                    outStream.Write(buffer, 0, length);
                    length = cryptoStream.Read(buffer, 0, buffer.Length);

                }
                this.progressPanel.Refresh();
                
                inStream.Close();
                outStream.Close();
                Log_Message_Decrypt(4);
            }
            catch
            {
                MessageBox.Show("Ошибка дешифрования!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 3;
        }


        // Rijndael
        private void Encrypt_Rijndael(String fName, String fNameCrypt, String passWord)
        {
            Log_Message_Encrypt(0);
            String fEncrypt_Rijndael = fName + ".Rijndael";

            if (fNameCrypt.Trim() != "")
            {
                fEncrypt_Rijndael = fNameCrypt;
            }

            try
            {
                SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("Rijndael");
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(1);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes(passWord + passWord +
                    passWord + passWord); // 32
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(passWord + passWord); // 16
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(2);
                FileStream outStream = File.OpenWrite(fEncrypt_Rijndael);
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(3);
                ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(outStream, transform, CryptoStreamMode.Write);
                Byte[] fNameRead = File.ReadAllBytes(fName);
                this.progressPanel.Refresh();
                
                cryptoStream.Write(fNameRead, 0, fNameRead.Length);
                this.progressPanel.Refresh();
                
                cryptoStream.Close();
                Log_Message_Encrypt(4);
            }
            catch
            {
                MessageBox.Show("Ошибка шифрования! Пароль доступа не верный!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 3;
        }


        private void Decrypt_Rijndael(String fName, String fNameCrypt, String passWord)
        {
            Log_Message_Decrypt(0);
            String decryptedPath = fName + ".Rijndaelnone";
            if (fNameCrypt.Trim() != "")
            {
                decryptedPath = fNameCrypt;
            }

            try
            {
                SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("Rijndael");
                this.progressPanel.Refresh();
                
                Log_Message_Decrypt(1);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes(passWord + passWord +
                    passWord + passWord); // 32
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(passWord + passWord); // 16
                this.progressPanel.Refresh();
                
                Log_Message_Decrypt(2);
                Log_Message_Decrypt(3);
                ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor();
                this.progressPanel.Refresh();
                
                Stream inStream = File.OpenRead(fName);
                CryptoStream cryptoStream = new CryptoStream(inStream, transform, CryptoStreamMode.Read);
                Byte[] buffer = new Byte[50];
                this.progressPanel.Refresh();
                
                int length = cryptoStream.Read(buffer, 0, buffer.Length);
                Stream outStream = File.OpenWrite(decryptedPath);
                while (length > 0)
                {
                    outStream.Write(buffer, 0, length);
                    length = cryptoStream.Read(buffer, 0, buffer.Length);

                }
                this.progressPanel.Refresh();
                
                inStream.Close();
                outStream.Close();
                Log_Message_Decrypt(4);
            }
            catch
            {
                MessageBox.Show("Ошибка дешифрования!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 3;
        }


        // TripleDES
        private void Encrypt_TripleDES(String fName, String fNameCrypt, String passWord)
        {
            Log_Message_Encrypt(0);
            String fEncrypt_TripleDES = fName + ".TripleDES";
            if (fNameCrypt.Trim() != "")
            {
                fEncrypt_TripleDES = fNameCrypt;
            }


            try
            {
                SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("TripleDES");
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(1);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes("U(*#$dfg" + passWord); // 16
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(passWord); // 8
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(2);
                FileStream outStream = File.OpenWrite(fEncrypt_TripleDES);
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(3);
                ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(outStream, transform, CryptoStreamMode.Write);
                Byte[] fNameRead = File.ReadAllBytes(fName);
                this.progressPanel.Refresh();
                
                cryptoStream.Write(fNameRead, 0, fNameRead.Length);
                cryptoStream.Close();
                this.progressPanel.Refresh();
                
                Log_Message_Encrypt(4);
            }
            catch
            {
                MessageBox.Show("Ошибка шифрования! Пароль доступа не верный!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 3;
        }


        private void Decrypt_TripleDES(String fName, String fNameCrypt, String passWord)
        {
            Log_Message_Decrypt(0);
            String decryptedPath = fName + ".TripleDESnone";
            if (fNameCrypt.Trim() != "")
            {
                decryptedPath = fNameCrypt;
            }

            try
            {
                SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("TripleDES");
                this.progressPanel.Refresh();
                
                Log_Message_Decrypt(1);
                symmetricAlgorithm.Key = ASCIIEncoding.ASCII.GetBytes("U(*#$dfg" + passWord); // 16
                symmetricAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(passWord); // 8
                this.progressPanel.Refresh();
                
                Log_Message_Decrypt(2);
                Log_Message_Decrypt(3);
                ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor();
                this.progressPanel.Refresh();
                
                Stream inStream = File.OpenRead(fName);
                CryptoStream cryptoStream = new CryptoStream(inStream, transform, CryptoStreamMode.Read);
                Byte[] buffer = new Byte[50];
                int length = cryptoStream.Read(buffer, 0, buffer.Length);
                this.progressPanel.Refresh();
                
                Stream outStream = File.OpenWrite(decryptedPath);
                while (length > 0)
                {
                    outStream.Write(buffer, 0, length);
                    length = cryptoStream.Read(buffer, 0, buffer.Length);

                }
                this.progressPanel.Refresh();
                
                inStream.Close();
                outStream.Close();
                this.progressPanel.Refresh();
                Log_Message_Decrypt(4);
            }
            catch
            {
                MessageBox.Show("Ошибка дешифрования!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Value = 3;
        }

        private void Log_Message_Encrypt(int LogNum)
        {
            switch (LogNum)
            {
                case 0:
                    richTextBox1.Text = "Инициализация шифрования...\nПуть к файлу для шифрования: " + 
                        textBox1.Text + "\nАлгоритм: " + comboBox2.Text + "\n";
                    progressBar2.Value = 1;
                    break;
                case 1:
                    richTextBox1.Text += "Генерация ключа и вектора для шифрования выбранного файла...\n";
                    progressBar2.Value = 2;
                    break;
                case 2:
                    richTextBox1.Text += "Инициализация выходного (зашифрованного) файла...\n";
                    progressBar2.Value = 3;
                    break;
                case 3:
                    richTextBox1.Text += "Процесс шифрования начался...\n";
                    progressBar2.Value = 4;
                    break;
                case 4:
                    richTextBox1.Text += "Процесс шифрования закончен!";
                    progressBar2.Value = 5;
                    break;
                default:
                    break;
            }
        }

        private void Log_Message_Decrypt(int LogNum)
        {
            switch (LogNum)
            {
                case 0:
                    richTextBox1.Text = "Инициализация дешифрования...\n\nПуть к файлу для дешифрования: " +
                        textBox1.Text + "\nАлгоритм: " + comboBox2.Text + "\n";
                    progressBar2.Value = 1;
                    break;
                case 1:
                    richTextBox1.Text += "Генерация ключа и вектора для дешифрования выбранного файла...\n";
                    progressBar2.Value = 2;
                    break;
                case 2:
                    richTextBox1.Text += "Инициализация выходного (дешифрованного) файла...\n";
                    progressBar2.Value = 3;
                    break;
                case 3:
                    richTextBox1.Text += "Процесс дешифрования начался...\n";
                    progressBar2.Value = 4;
                    break;
                case 4:
                    richTextBox1.Text += "Процесс дешифрования закончен!";
                    progressBar2.Value = 5;
                    break;
                default:
                    break;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "Шифрование";
            comboBox2.Text = "DES";

            progressPanel.Show();
            workPanel.Show();
            welcomePanel.Show();



        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("help.chm");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult OProgramme = MessageBox.Show("МАСТЕР шифрования файлов предназначен для шифрования/дешифрования файлов.\n\n" + 
                "Более подробная информация о программе - в справке.\n" + 
                "Перейти в справку?", "О программе...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (OProgramme == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("help.chm");
            }

        }

        private void workPanel_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = welcomeMessage;
        }

        private void comboBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = tipRaboti;
        }

        private void comboBox2_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = algoritm;

        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = putKFailu;

        }

        private void textBox2_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = parolDostupa;
            
        }

        private void textBox3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = putVihod;
        }

        private void progressPanel_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = welcomeMessage;
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = log;
        }

        private void progressBar2_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = processBar;
        }















    }
}
