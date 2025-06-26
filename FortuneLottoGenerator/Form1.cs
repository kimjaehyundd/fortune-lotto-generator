using System;
using System.Drawing;
using System.Windows.Forms;

namespace FortuneLottoGenerator
{
    public partial class Form1 : Form
    {
        private FortuneInterpreter interpreter;
        private LottoNumberGenerator generator;

        public Form1()
        {
            InitializeComponent();
            interpreter = new FortuneInterpreter();
            generator = new LottoNumberGenerator();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string story = txtStory.Text.Trim();
            
            if (string.IsNullOrEmpty(story))
            {
                MessageBox.Show("오늘이나 어제 있었던 일에 대해 이야기해 주세요!", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 사주팔자 해석
                var interpretation = interpreter.InterpretStory(story);
                
                // 로또번호 생성
                var numbers = generator.GenerateNumbers(interpretation);
                
                // 결과 표시
                DisplayResults(interpretation, numbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayResults(FortuneInterpretation interpretation, int[] numbers)
        {
            // 사주팔자 해석 결과 표시
            txtInterpretation.Text = interpretation.Explanation;
            
            // 로또번호 표시
            lblNumbers.Text = $"운명의 로또번호: {string.Join(", ", numbers)}";
            
            // 로또번호를 각각 다른 색으로 표시
            DisplayColoredNumbers(numbers);
        }

        private void DisplayColoredNumbers(int[] numbers)
        {
            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Purple, Color.Brown };
            
            for (int i = 0; i < numbers.Length && i < numberLabels.Length; i++)
            {
                numberLabels[i].Text = numbers[i].ToString();
                numberLabels[i].BackColor = colors[i % colors.Length];
                numberLabels[i].ForeColor = Color.White;
                numberLabels[i].Visible = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStory.Clear();
            txtInterpretation.Clear();
            lblNumbers.Text = "이곳에 운명의 로또번호가 나타납니다.";
            
            foreach (var label in numberLabels)
            {
                label.Visible = false;
            }
        }

        private void txtStory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                btnGenerate_Click(sender, e);
            }
        }
    }
}