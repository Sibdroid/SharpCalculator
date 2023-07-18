using NCalc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
		bool isAdvancedChanged = false;
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
			RootButton.Click += funcButtonClick;
			SquareButton.Click += funcButtonClick;
			CubeButton.Click += funcButtonClick;
			TwoPowerButton.Click += funcButtonClick;
			TenPowerButton.Click += funcButtonClick;
			DarkSwitch.CheckedChanged += darkMode;
			SwitchBox.Click += ChangeForm;
			SwitchButton.Click += ChangeForm;
			SwitchButton.FlatAppearance.MouseOverBackColor = SwitchButton.BackColor;
			SwitchBox.FlatAppearance.MouseOverBackColor = SwitchBox.BackColor;
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
		private void funcButtonClick(object sender, EventArgs e)
		{
			System.Windows.Forms.Button button = sender as System.Windows.Forms.Button;
			string name = button.Name;
			string argument1 = "";
			string argument2 = "";
			string function = "Pow";
			switch (name)
			{
				case "RootButton":
					argument1 = ResultLabel.Text;
					argument2 = "0.5";
					break;
				case "SquareButton":
					argument1 = ResultLabel.Text;
					argument2 = "2";
					break;
				case "CubeButton":
					argument1 = ResultLabel.Text;
					argument2 = "3";
					break;
				case "TwoPowerButton":
					argument1 = "2";
					argument2 = ResultLabel.Text;
					break;
				case "TenPowerButton":
					argument1 = "10";
					argument2 = ResultLabel.Text;
					break;

			}
			Tools.calculateFunc(ResultLabel, PreviewLabel, function, argument1, argument2);	

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
		private void darkMode(object sender, EventArgs e)
		{
			// Graphics graphics = this.CreateGraphics();
			// PaintEventArgs p = new PaintEventArgs(graphics, AdvancedSwitch.rect);
			// p.Graphics.FillEllipse(Brushes.Blue, AdvancedSwitch.rect);
			foreach (var button in this.Controls.OfType<System.Windows.Forms.Button>())
			{
				if (button.FlatStyle.ToString() == "Flat")
				{
					if (DarkSwitch.Checked)
					{
						button.BackColor = ColorTranslator.FromHtml("#0d3370");
						button.ForeColor = Color.White;
					}
					else
					{
						button.BackColor = ColorTranslator.FromHtml("#f2cc8f");
						button.ForeColor = Color.Black;
					}
				}
				else
				{
					if (DarkSwitch.Checked)
					{
						button.BackColor = ColorTranslator.FromHtml("#1e1e1e");
						button.ForeColor = Color.White;
					}
					else
					{
						button.BackColor = ColorTranslator.FromHtml("#e1e1e1");
						button.ForeColor = Color.Black;
					}
				}
			}
			if (DarkSwitch.Checked)
			{
				this.BackColor = ColorTranslator.FromHtml("#0f0f0f");
				ResultLabel.ForeColor = Color.White;
				PreviewLabel.ForeColor = Color.White;
				AdvancedHint.ForeColor = Color.White;
				DarkHint.ForeColor = Color.White;
			}
			else
			{
				this.BackColor = ColorTranslator.FromHtml("#f0f0f0");
				ResultLabel.ForeColor = Color.Black;
				PreviewLabel.ForeColor = Color.Black;
				AdvancedHint.ForeColor = Color.Black;
				DarkHint.ForeColor = Color.Black;
			}
		}
		private void ChangeForm(object sender, EventArgs e)
		{
			int margin = 4;
			if (this.isAdvancedChanged)
			{
				this.isAdvancedChanged = false;
				SwitchButton.Location = new Point(SwitchBox.Location.X + margin, SwitchButton.Location.Y);
				SwitchButton.BackColor = ColorTranslator.FromHtml("#a9a9a9");
				this.Size = new Size(275, 665);
				ResultLabel.Size = new Size(258, 85);
				PreviewLabel.Size = new Size(258, 50);

			}
			else
			{
				this.isAdvancedChanged = true;
				int switchBoxEnd = SwitchBox.Location.X + SwitchBox.Size.Width;
				SwitchButton.Location = new Point(switchBoxEnd - SwitchButton.Size.Width - margin,
												  SwitchButton.Location.Y);
				SwitchButton.BackColor = ColorTranslator.FromHtml("#ff5154");
				this.Size = new Size(355, 665);
				ResultLabel.Size = new Size(336, 85);
				PreviewLabel.Size = new Size(336, 50);
			}
			SwitchButton.FlatAppearance.MouseOverBackColor = SwitchButton.BackColor;
			SwitchBox.FlatAppearance.MouseOverBackColor = SwitchBox.BackColor;
		}

	}
	public class ToggleSwitch : CheckBox
	{
		public int a { get; set; }
		int d = 0;
		int r = 0;
		Rectangle rect = new Rectangle();
		SolidBrush activeBrush = new SolidBrush(Color.White);
		SolidBrush activeBrushLight = new SolidBrush(Color.FromArgb(255, (byte)255, (byte)81, (byte)84));
		SolidBrush activeBrushDark = new SolidBrush(Color.FromArgb(255, (byte)0, (byte)174, (byte)171));
		public ToggleSwitch()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			Padding = new Padding(6);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			this.OnPaintBackground(e);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (var path = new GraphicsPath())
			{
				d = Padding.All;
				r = this.Height - 2 * d;
				path.AddArc(d, d, r, r, 90, 180);
				path.AddArc(this.Width - r - d, d, r, r, -90, 180);
				e.Graphics.FillPath(Checked ? Brushes.DarkGray : Brushes.LightGray, path);
				r = Height - 1;
				rect = Checked ? new Rectangle(Width  - r - 1, 0, r, r) : new Rectangle(0, 0, r, r);
				CheckBox darkSwitch = System.Windows.Forms.Application.OpenForms["Form1"].Controls["DarkSwitch"] as CheckBox;
				if (darkSwitch.Checked)
				{
					activeBrush = activeBrushDark;
				}
				else
				{
					activeBrush = activeBrushLight;
				}
				e.Graphics.FillEllipse(Checked ? activeBrush : Brushes.DarkGray, rect);
			}
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
		public static void getPowerExpression(Label label, string function,
			                                  string argument1, string argument2)
		{
			label.Text = $"{function}({argument1}| {argument2})";
		}
		public static void calculateFunc(Label label, Label preview, string function, 
			                             string argument1, string argument2)
		{
			label.Text = $"{function}({argument1}| {argument2})";
			Console.WriteLine(label.Text);
			calculateResult(label, preview);
			int first = preview.Text.IndexOf('(');
			int last = preview.Text.LastIndexOf(')');
			string arguments = preview.Text.Substring(first + 1, last - first - 1);
			arguments = arguments.Replace(", ", ",");
			string[] divided = arguments.Split(',');
			preview.Text = $"{divided[0]}^{divided[1]}";
		}
		public static void calculateResult(Label label, Label preview)
		{
			bool changedPreview = false;
			if (label.Text.Contains("^"))
			{
				string[] arguments = label.Text.Split('^');
				label.Text = $"Pow({arguments[0]}| {arguments[1]})";
				preview.Text = $"{arguments[0]}^{arguments[1]}";
				changedPreview = true;
			}
			label.Text = label.Text.Replace(",", ".");
			label.Text = label.Text.Replace("|", ",");
			Expression expression = new Expression(label.Text);
			if (!changedPreview)
			{
				preview.Text = label.Text;
			}
	
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

