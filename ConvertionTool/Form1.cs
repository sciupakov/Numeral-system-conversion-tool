using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ConvertionTool
{
    public partial class Form1 : Form
    {
        string binary = String.Empty;
        string[] numFract;
        uint signs;
        string text;
        char[] RevStr;
        StringBuilder builder;
        static readonly Regex BinValidator = new Regex(@"^[01]+[,\.]?[01]*?$");
        static readonly Regex OctValidator = new Regex(@"^[0-7]+[,\.]?[0-7]*?$");
        static readonly Regex DecValidator = new Regex(@"^[0-9]+[,\.]?[0-9]*?$");
        static readonly Regex HexValidator = new Regex(@"^[0123456789ABCDEF]+[,\.]?[0123456789ABCDEF]*?$");
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtOutput.ReadOnly = true;
            txtOutput.BorderStyle = 0;
            txtOutput.BackColor = this.BackColor;
            txtOutput.TabStop = false;

            cmbInput.SelectedIndex = 3;
            cmbOutput.SelectedIndex = 1;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            text = (txtInput.Text).ToUpper();
            text = text.Trim();

            if (!uint.TryParse(txtSigns.Text, out signs))
            {
                MessageBox.Show("Number of signs after comma is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (signs > 20000)
            {
                MessageBox.Show("There is too much signs after comma! Your screen wouldn't be able to display them all anyway.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            
            switch (cmbInput.SelectedIndex)
            {

                case 0:
                    if (BinValidator.IsMatch(text))
                    {
                        binary = text;
                    }
                    else
                    {
                        MessageBox.Show("Input number is invalid! The input system does not match input parameter or parameter has syntax error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    break;
                case 1:
                    if (OctValidator.IsMatch(text))
                    {
                        binary = Convertion.OctToBinary(text);
                    }
                    else
                    {
                        MessageBox.Show("Input number is invalid! The input system does not match input parameter or parameter has syntax error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    break;
                case 2:
                    if (DecValidator.IsMatch(text))
                    {
                        if (cmbOutput.SelectedIndex == 1)
                        {
                            binary = Convertion.DecimalToBinary(text, signs * 3);
                        }
                        else if (cmbOutput.SelectedIndex == 3)
                        {
                            binary = Convertion.DecimalToBinary(text, signs * 4);
                        }
                        else
                        {
                            binary = Convertion.DecimalToBinary(text, signs);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Input number is invalid! The input system does not match input parameter or parameter has syntax error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                    break;
                case 3:
                    if (HexValidator.IsMatch(text))
                    {
                        binary = Convertion.HexToBinary(text);
                    }
                    else
                    {
                        MessageBox.Show("Input number is invalid! The input system does not match input parameter or parameter has syntax error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    break;
                default:
                    break;
            }

            switch (cmbOutput.SelectedIndex)
            {
                case 0:
                    if (cmbInput.SelectedIndex == 0)
                    {
                        txtOutput.Text = text;
                    }
                    else
                    {
                        txtOutput.Text = FormatNumber(binary);
                    }
                    return;
                case 1:
                    if (cmbInput.SelectedIndex == 1)
                    {
                        txtOutput.Text = text;
                    }
                    else
                    {
                        txtOutput.Text = FormatNumber(Convertion.BinaryToOctal(binary));
                    }
                    return;
                case 2:
                    if (cmbInput.SelectedIndex == 2)
                    {
                        txtOutput.Text = text;
                    }
                    else
                    {
                        txtOutput.Text = FormatNumber(Convertion.BinaryToDecimal(binary));
                    }
                    return;
                case 3:
                    if (cmbInput.SelectedIndex == 3)
                    {
                        txtOutput.Text = text.ToUpper();
                    }
                    else
                    {
                        txtOutput.Text = FormatNumber(Convertion.BinaryToHex(binary));
                    }

                    return;

                default:
                    break;
            }
            
        }

        private string FormatNumber(string num)
        {
            builder = new StringBuilder();
            num = num.TrimStart('0');
            if (num[0] == ',' || num[0] == '.')
            {
                num = "0" + num;
            }
            if (num.Contains(",") || num.Contains("."))
            {
                num = num.TrimEnd('0');
                
            }
            numFract = num.Split(',', '.');
            numFract[0] = ReverseString(numFract[0]);
            numFract[0] = Regex.Replace(numFract[0], ".{4}", "$0 ");
            numFract[0] = ReverseString(numFract[0]);
            builder.Append(numFract[0]);
            if (numFract.Length == 2)
            {
                builder.Append(", ");

                builder.Append(Regex.Replace(numFract[1], ".{4}", "$0 "));
            }
            Console.WriteLine(builder.ToString());
            return builder.ToString();
        }

        private string ReverseString(string str)
        {                        
            RevStr = str.ToCharArray();
            Array.Reverse(RevStr);
            return new string(RevStr);
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.BackColor = 
        }
    }
}

