using TinyDemo.SharedLib.Entities;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.WFClient
{
    public partial class MainForm : Form
    {
        ILottoService _lottoService;
        Lotto _lotto;
        public MainForm(ILottoService lottoService)
        {
            _lottoService = lottoService;
            InitializeComponent();
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            await GenerateLottoNumbers();
        }

        private async Task GenerateLottoNumbers()
        {
            try
            {
                btnGenerate.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                tsStatus.Text = "Generating lotto numbers. Please wait....";
                _lotto = await _lottoService.GenerateLotto();
                setNumbers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsStatus.Text = "Something went wrong!";
            }
            finally
            {
                btnGenerate.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void setNumbers()
        {
            if (_lotto != null && _lotto.Numbers.Count == 7)
            {
                numberControl1.Number = _lotto.Numbers[0];
                numberControl2.Number = _lotto.Numbers[1];
                numberControl3.Number = _lotto.Numbers[2];
                numberControl4.Number = _lotto.Numbers[3];
                numberControl5.Number = _lotto.Numbers[4];
                numberControl6.Number = _lotto.Numbers[5];
                numberControl7.Number = _lotto.Numbers[6];
                tsStatus.Text = $"Lotto numbers generated on {_lotto.GeneratedOnTime.ToShortTimeString()}";
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await GenerateLottoNumbers();
        }
    }
}
