using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public unsafe static class BinaryOperators
        {
            public static uint ROL(uint x, int n)
            {
                n %= sizeof(uint) * 8;
                return (x << n) + (x >> (sizeof(uint) * 8 - n));
            }

            public static uint ROR(uint x, int n)
            {
                n %= sizeof(uint) * 8;
                return (x >> n) + (x << (sizeof(uint) * 8 - n));
            }
        }
        [STAThread]
        static void Main(string[] args)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            long fsize = 0, rest = 0, sizeforread = 0;
                            myStream.Seek(0, SeekOrigin.End);
                            fsize = myStream.Position * 8;
                            sizeforread = fsize / 8;
                            Console.WriteLine(fsize);
                            myStream.Seek(0, SeekOrigin.Begin);
                            rest = fsize % (sizeof(long) * 8);
                            Console.WriteLine(rest);
                            fsize += sizeof(long) * 8 - rest;
                            Console.WriteLine(fsize);

                            char[] msgSrc = new char[fsize];
                            char[] msgEncr = new char[fsize];
                            for (int i = 0, stop = 0; i < msgSrc.Length; i++, stop++)
                            {
                                if (stop >= sizeforread) msgSrc[i] = '0';
                                else msgSrc[i] = Convert.ToChar(myStream.ReadByte());
                            }
                            for (int i = 0; i < msgSrc.Length; i++)
                            {
                                Console.Write(msgSrc[i]);
                            }
                            myStream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            Console.WriteLine("ROL 3 on 2 is " + BinaryOperators.ROL(3, 2));
            Console.WriteLine("ROR 3 on 2 is " + BinaryOperators.ROR(3, 2));
            Console.ReadKey();
        }
    }
}