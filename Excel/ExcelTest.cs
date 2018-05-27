using ExcelControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel
{
    public partial class ExcelTest : Form
    {
        public ExcelTest()
        {
            InitializeComponent();
            test();
        }

        public void GetExcel()
        {
            FromExcel e = new FromExcel();
            //var dt = e.OpenExcel(@"C:\Users\ronal\Desktop\excel\GoodsCode_2018_04_26_033012.xlsx");
            //var dt = e.OpenExcel(@"C:\Users\ronal\Desktop\test상품명수집_2018_05_10_155618.xlsx");

            string err = string.Empty;
            //var dt = e.OpenExcel(@"C:\Users\ronal\Desktop\test상품명수집_2018_05_10_155618.xlsx", ref err);
            //var dt = e.OpenExcel(@"C:\Users\ronal\Desktop\미동의테스트.xlsx");
            var dt = e.OpenExcel(@"C:\Users\ronal\Desktop\공백테스트 - 복사본.xlsx", ref err);


            ;
            List<TestDataModel> model = new List<TestDataModel>();
            
            foreach (DataRow row in dt.Rows)
            {

                TestDataModel t = new TestDataModel()
                {
                    GooodsCode = row[0].ToString(),
                    NameCN = row[1].ToString(),
                    NameEN = row[2].ToString(),
                    NameJP = row[3].ToString(),
                    NameKOR = row[4].ToString()
                };
                model.Add(t);
            }

            ;

        }

        public void test()
        {
            var testDataModel = new TestDataModel()
            {
                GooodsCode = "11111",
                NameCN = "",
                NameEN = "NameEN",
                NameJP = "NameJP",
                NameKOR = "NameKOR"
            };

            var testDataModel2 = new TestDataModel()
            {
                GooodsCode = "22222",
                NameCN = "NameCN2",
                NameEN = "",
                NameJP = "NameJP2",
                NameKOR = "NameKOR2"
            };

            var testDataModel3 = new TestDataModel()
            {
                GooodsCode = "33333",
                NameCN = "NameCN2",
                NameEN = "NameEN2",
                NameJP = "",
                NameKOR = "NameKOR2"
            };

            var testDataModel4 = new TestDataModel()
            {
                GooodsCode = "44444",
                NameCN = "NameCN2",
                NameEN = "",
                NameJP = "NameJP2",
                NameKOR = ""
            };

            var testDataModel5 = new TestDataModel()
            {
                GooodsCode = "",
                NameCN = "NameCN2",
                NameEN = "",
                NameJP = "NameJP2",
                NameKOR = ""
            };

            var dataList = new List<TestDataModel>();
            dataList.Add(testDataModel);
            dataList.Add(testDataModel2);
            dataList.Add(testDataModel3);
            dataList.Add(testDataModel4);
            dataList.Add(testDataModel5);

            var excel = new ToExcel();
            var streamData = excel.DataToExcel<List<TestDataModel>>(dataList, "공백테스트");

            System.IO.File.WriteAllBytes(@"C:\Users\ronal\Desktop\excel\test.xlsx", streamData);
            
        }
    }

    public class TestDataModel
    {
        /// <summary>
        /// 상품 코드
        /// </summary>
        [Display(Name = "상품코드")]
        public string GooodsCode { get; set; }

        /// <summary>
        /// 국문 상품명
        /// </summary>
        [Display(Name = "국문")]
        public string NameKOR { get; set; }

        /// <summary>
        /// 영문 상품명
        /// </summary>
        [Display(Name = "영문")]
        public string NameEN { get; set; }

        /// <summary>
        /// 일문 상품명
        /// </summary>
        [Display(Name = "일문")]
        public string NameJP { get; set; }

        /// <summary>
        /// 중문 상품명
        /// </summary>
        [Display(Name = "중문")]
        public string NameCN { get; set; }
    }
}
