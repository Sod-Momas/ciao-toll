using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ciao_toll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void recalculate()
        {
            // 清理警告
            clean_alert();
            // 获取公差
            var percent = getPercent();

            string result_str = getResult(percent);
            if (tb_result != null)
            {
                tb_result.Text = result_str;
                Clipboard.SetDataObject(result_str);
            }
        }

        private string getResult(int percent)
        {
            var raw = tb_detail.Text.Trim();
            string[] arr = raw.Split(";");
            List<string> result = new();


            if (arr.Length == 0)
            {
                alert("数据输入错误");
                return "";
            }

            var err = "";
            foreach (var item in arr)
            {
                try
                {
                    result.Add(Math.Round(double.Parse(item) * percent / 100, 1).ToString("F1"));
                }
                catch
                {
                    err += item;
                    continue;
                }
            }
            if (!string.IsNullOrEmpty(err))
            {
                alert("无法理解的输入：" + err);
            }
            if (result.Count < 1)
            {
                return "";
            }

            string string_builder = String.Join("、", result.ToArray());

            return string_builder;
        }

        private void clean_alert()
        {
            if (tb_alert != null)
            {
                tb_alert.Text = "";
            }
        }

        private void alert(string v)
        {
            tb_alert.Text = v;
        }

        private int getPercent(int default_value = 100)
        {
            // 计算公差，如果获取到错误的数值，则默认为100%
            try
            {
                return int.Parse(this.tb_percent.Text.Trim());
            }
            catch
            {
                alert("公差输入错误，自动转换为100%");
                return 100;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            recalculate();
        }
    }
}
