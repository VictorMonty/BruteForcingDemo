﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BruteForcingDemo
{
    public partial class Form1 : Form
    {
        // Variables for brute forcing code
        static string password;
        static string passGuess;
        static bool isMatched = false;
        static int basicPassLength = 0;
        static long computedKeys = 0;
        static double endTime = 0;

        static char[] basicPass =
        {
            '0','1','2','3','4','5','6','7','8','9','a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        // Generates new WinForms GUI for later.
        TextBox textBox1 = new TextBox();
        Button button3 = new Button();
        Label label3 = new Label();
        
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            label2.Text = "Please enter a password: ";

            button1.Hide();
            button2.Hide();
                        
            textBox1.Size = new Size(300, 20);
            textBox1.Location = new Point(45, 110);
            this.Controls.Add(textBox1);


            button3.Text = "START";
            button3.Location = new Point(150, 150);
            this.Controls.Add(button3);

            // Binds new button3 generated by clicking button1 to button3_Click event handler below.
            button3.Click += new EventHandler(button3_Click);


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // This is the event handler for button3, which initializes the brute forcing code.
        private void button3_Click(object sender, EventArgs e)
        {
            isMatched = false;
            var startTime = DateTime.Now;

            // Grabs user input from input box.
            password = textBox1.Text;
            password.ToString();

            // Brute forcing code integration w/ WinForms begins here.
            basicPassLength = basicPass.Length;

            var estimatedPassLength = 0;
            while (!isMatched)
            {
                estimatedPassLength++;
                startInitialize(estimatedPassLength);
            }

            void startInitialize(int keyLength)
            {
                var keyChars = createCharArray(keyLength, basicPass[0]);
                var indexedLastChar = keyLength - 1;
                newKey(0, keyChars, keyLength, indexedLastChar);
            }

            char[] createCharArray(int Length, char defaultChar)
            {
                return (from c in new char[Length] select defaultChar).ToArray();
            }

            void newKey(int currentCharPosition, char[] keyChars, int keyLength, int indexedLastChar)
            {
                var nextCharPosition = currentCharPosition + 1;
                for (int i = 0; i < basicPass.Length; i++)
                {
                    keyChars[currentCharPosition] = basicPass[i];
                    if (currentCharPosition < indexedLastChar)
                    {
                        newKey(nextCharPosition, keyChars, keyLength, indexedLastChar);
                    }
                    else
                    {
                        computedKeys++;
                        if ((new string(keyChars) == password))
                        {
                            if (!isMatched)
                            {
                                endTime = DateTime.Now.Subtract(startTime).TotalSeconds;
                                isMatched = true;
                                passGuess = new string(keyChars);
                                label1.Text = "Password matched. Your password is: " + password +
                                "\nTime passed: " + Math.Truncate(endTime) + " seconds" +
                                "\n \nYour password is " + estimatedPassLength + " characters long." +
                                "\nNumber of password combinations attempted: \t" + computedKeys;
                            }
                            return;
                        }
                    }
                }
            }

        }

        // Button on main page to exit the application after launch.
        public void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
