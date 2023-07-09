using NCalc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		bool isChanged = false;
		public Form1()
		{
			InitializeComponent();
			this.KeyPreview = true;
			this.KeyDown += new KeyEventHandler(Form1_KeyDown);
			NumButton0.Click += buttonClick;
			NumButton1.Click += buttonClick;
			NumButton2.Click += buttonClick;
			NumButton3.Click += buttonClick;
			NumButton4.Click += buttonClick;
			NumButton5.Click += buttonClick;
			NumButton6.Click += buttonClick;
			NumButton7.Click += buttonClick;
			NumButton8.Click += buttonClick;
			NumButton9.Click += buttonClick;
			AddButton.Click += buttonClick;
			SubButton.Click += buttonClick;
			MulButton.Click += buttonClick;
			DivButton.Click += buttonClick;
			PointButton.Click += buttonClick;
			PowerButton.Click += buttonClick;
			LeftBracketButton.Click += buttonClick;
			RightBracketButton.Click += buttonClick;
		}

		private void buttonClick(object sender, EventArgs e)
		{
			if (!isChanged)
			{
				ResultLabel.Text = "";
				isChanged = true;
			}
			System.Windows.Forms.Button button = sender as System.Windows.Forms.Button;
			string text = button.Text;
			ResultLabel.Text += text;
			Tools.resizeFont(ResultLabel);
		}
		private void ClearButton_Click(object sender, EventArgs e)
		{
			Tools.clear(ResultLabel, PreviewLabel);
			isChanged = false;
		}

		private void ResultButton_Click(object sender, EventArgs e)
		{
			Tools.calculateResult(ResultLabel, PreviewLabel);
		}

		private void EraseButton_Click(object sender, EventArgs e)
		{
			Tools.erase(ResultLabel);
			if (ResultLabel.Text.Length == 0)
			{
				ResultLabel.Text = "0";
				isChanged = false;
			}
			ResultLabel.Text = "";
		}
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			string text = e.KeyCode.ToString();
			string value = "";
			bool overwriteChange = false;
			if (Control.ModifierKeys == Keys.Shift)
			{
				switch (text)
				{
					case "D6":
						value = "^";
						break;
					case "D8":
						value = "*";
						break;
					case "D9":
						value = "(";
						break;
					case "D0":
						value = ")";
						break;
					case "Oemplus":
						value = "+";
						break;
					case "OemMinus":
						value = "-";
						break;
					case "OemQuestion":
						value = ".";
						break;
					case "OemPeriod":
						value = ".";
						break;
					default:
						overwriteChange = true;
						break;
				}
			}
			else
			{
				switch (text)
				{
					case "Add":
						value = "+";
						break;
					case "Subtract":
						value = "-";
					    break;
					case "Multiply":
						value = "*";
						break;
					case "Divide":
						value = "/";
						break;
					case "Decimal":
						value = ".";
						break;
					case "OemMinus":
						value = "-";
						break;
					case "OemQuestion":
						value = ".";
						break;
					case "OemPeriod":
						value = ".";
						break;
					case "Back":
						Tools.erase(ResultLabel);
						if (ResultLabel.Text.Length == 0)
						{
							ResultLabel.Text = "0";
							isChanged = false;
							overwriteChange = true;
						}
						break;
					case "C":
						Tools.clear(ResultLabel, PreviewLabel);
						isChanged = false;
						overwriteChange = true;
						break;
					case "Oemplus":
						Tools.calculateResult(ResultLabel, PreviewLabel);
						overwriteChange = true;
						break;
					default:
						if (text.Length == 2 && text[0].ToString() == "D")
						{
							value = text[1].ToString();
						}	
						else if (text.Length == 7 && text.Contains("NumPad"))
						{
							value = text[text.Length - 1].ToString();
						}
						else
						{
							overwriteChange = true;
							Console.WriteLine(text);
						}
						break;
				}
			}
			if (!isChanged && !overwriteChange)
			{
				ResultLabel.Text = "";
				isChanged = true;
			}
			ResultLabel.Text += value;
			Tools.resizeFont(ResultLabel);
		}

		private void PlusMinusButton_Click(object sender, EventArgs e)
		{
			if (ResultLabel.Text.StartsWith("-"))
			{
				ResultLabel.Text = ResultLabel.Text.Remove(0, 1);
			}
			else
			{
				ResultLabel.Text = "-" + ResultLabel.Text;
			}
		}

		private void ReverseDivButton_Click(object sender, EventArgs e)
		{
			ResultLabel.Text = "1/" + ResultLabel.Text;
			Tools.calculateResult(ResultLabel, PreviewLabel);
		}

		private void SquareButton_Click(object sender, EventArgs e)
		{
			ResultLabel.Text = $"Pow({ResultLabel.Text}| 2)";
			Tools.calculateResult(ResultLabel, PreviewLabel);
			int first = PreviewLabel.Text.IndexOf('(');
			int last = PreviewLabel.Text.LastIndexOf(')');
			string arguments = PreviewLabel.Text.Substring(first + 1, last - first - 1);
			arguments = arguments.Replace(", ", ",");
			string[] divided = arguments.Split(',');
			PreviewLabel.Text = $"{divided[0]}^{divided[1]}";
		}
	}
	public class Tools
	{
		public static float defaultSize = 40.0f;
		public static void resizeFont(Label label)
		{
			var size = default(SizeF);
			do
			{
				using (var font = new Font(label.Font.Name, label.Font.SizeInPoints))
				{
					size = TextRenderer.MeasureText(label.Text, font);
					if (size.Width > label.Width)
					{
						label.Font = new Font(font.Name, font.SizeInPoints - 1f);
					}
				}
			} while (size.Width > label.Width);
		}
		public static void erase(Label label)
		{
			label.Text = label.Text.Remove(label.Text.Length - 1);
			resizeFont(label);

		}
		public static void clear(Label label, Label preview)
		{
			label.Text = "0";
			preview.Text = "";
			var font = new Font(label.Font.Name, label.Font.SizeInPoints);
			label.Font = new Font(font.Name, defaultSize);
		}
		public static void calculateResult(Label label, Label preview)
		{
			if (label.Text.Contains("^"))
			{
				string[] arguments = label.Text.Split('^');
				label.Text = $"Pow({arguments[0]}| {arguments[1]})";
			}
			label.Text = label.Text.Replace(",", ".");
			label.Text = label.Text.Replace("|", ",");
			Expression expression = new Expression(label.Text);
			preview.Text = label.Text;
			try
			{
				label.Text = Convert.ToString(expression.Evaluate());
				label.Text = label.Text.Replace(",", ".");
				foreach (char c in label.Text)
				{
					if (!"0123456789.-".Contains(c))
					{
						label.Text = "#";
						break;
					}
				}
			}
			catch (EvaluationException e)
			{
				label.Text = "#";
			}
			resizeFont(label);
			resizeFont(preview);
		}
	}
}  

