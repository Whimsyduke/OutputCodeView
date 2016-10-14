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

namespace OutputCodeView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum CodeForSystem
        {
            RWS,
            RES,
        };
        public MainWindow()
        {
            InitializeComponent();
            foreach (string select in Enum.GetNames(typeof(CodeForSystem)))
            {
                SelectText.Items.Add(select);
            }
            SelectText.SelectedIndex = 0;
        }

        private void SelectText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CodeForSystem type = (CodeForSystem)SelectText.SelectedIndex;
            string codes = "\r\n";
            if (type == CodeForSystem.RES)
            {
                #region AES加密系统
                int[][] confusionMatrix = new int[4][]
                {
                    new int[4] { 0x02, 0x03, 0x01, 0x01 },
                    new int[4] { 0x01, 0x02, 0x03, 0x01 },
                    new int[4] { 0x01, 0x01, 0x02, 0x03 },
                    new int[4] { 0x03, 0x01, 0x01, 0x02 },
                };

                int[][] inverseConfusionMatrix = new int[4][]
                {
                    new int[4] { 0x0E, 0x0B, 0x0D, 0x09 },
                    new int[4] { 0x09, 0x0E, 0x0B, 0x0D },
                    new int[4] { 0x0D, 0x09, 0x0E, 0x0B },
                    new int[4] { 0x0B, 0x0D, 0x09, 0x0E },
                };
                int[] sbox = new int[256] {
	            // 0     1     2     3     4     5     6     7     8     9     a     b     c     d     e     f
	            0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, // 0
	            0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, // 1
	            0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, // 2
	            0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, // 3
	            0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, // 4
	            0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, // 5
	            0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, // 6
	            0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, // 7
	            0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73, // 8
	            0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb, // 9
	            0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, // a
	            0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, // b
	            0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, // c
	            0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, // d
	            0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, // e
	            0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16};// f
                int[] isbox = new int[256] {
                    // 0     1     2     3     4     5     6     7     8     9     a     b     c     d     e     f
	            0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb, // 0
	            0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb, // 1
	            0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e, // 2
	            0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25, // 3
	            0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92, // 4
	            0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84, // 5
	            0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06, // 6
	            0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b, // 7
	            0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73, // 8
	            0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e, // 9
	            0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b, // a
	            0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4, // b
	            0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f, // c
	            0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef, // d
	            0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61, // e
	            0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d};// f

                codes += "void LibWH_FUNC_RES_InitialFunction()\r\n{\r\n";

                codes += "// Generation columns Confusion matrix. \r\n";
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        codes += "libWH_GV_RES_ColumnsConfusionVector_" + i + "[" + j + "] = " + confusionMatrix[i][j] + ";\r\n";
                    }
                    codes += "libWH_GV_RES_ColumnsConfusionMatrix" + i + " = libWH_GV_RES_ColumnsConfusionVector_" + i + ";\r\n";
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        codes += "libWH_GV_RES_InverseColumnsConfusionVector_" + i + "[" + j + "] = " + inverseConfusionMatrix[i][j] + ";\r\n";
                    }
                    codes += "libWH_GV_RES_InverseColumnsConfusionMatrix" + i + " = libWH_GV_RES_InverseColumnsConfusionVector_" + i + ";\r\n";
                }

                codes += "// ASCII char to integer maping \r\n";
                for (int i = 0; i < 256; i++)
                {
                    codes += "DataTableSetInt(true, libWH_GVC_RES_NameASCIIToInt + \"\\x" + i.ToString("X2") + "\", " + i.ToString() + ");\r\n";
                }

                codes += "\r\n// ASCII integer to char maping \r\n";
                for (int i = 0; i < 256; i++)
                {
                    codes += "DataTableSetString(true, libWH_GVC_RES_NameASCIIToChar + \"" + i.ToString() + "\", \"\\x" + i.ToString("X2") + "\");\r\n";
                }

                codes += "\r\n// S-box transformation table \r\n";
                for (int i = 0; i < 256; i++)
                {
                    codes += "libWH_GV_RES_RijndaelEncryptionSboxTransformationTable[" + i.ToString() + "] = 0x" + sbox[i].ToString("X2") + ";\r\n";
                }

                codes += "\r\n// Inverse S-box transformation table \r\n";
                for (int i = 0; i < 256; i++)
                {
                    codes += "libWH_GV_RES_RijndaelEncryptionInverseSboxTransformationTable[" + i.ToString() + "] = 0x" + isbox[i].ToString("X2") + ";\r\n";
                }

                #endregion
            }
            if (type == CodeForSystem.RWS)
            {
                #region 随机区域系统
                // 各Cube类型门数量
                int[] typeGateCount = new int[9] { 4, 12, 12, 8, 8, 8, 8, 12, 12 };
                // 各Cube类型各方向门数量
                int[][] sideGateCount = new int[9][]
                {
                new int [4] { 1, 1, 1, 1 },
                new int [4] { 3, 3, 3, 3 },
                new int [4] { 3, 3, 3, 3 },
                new int [4] { 3, 1, 3, 1 },
                new int [4] { 1, 3, 1, 3 },
                new int [4] { 3, 1, 3, 1 },
                new int [4] { 1, 3, 1, 3 },
                new int [4] { 3, 3, 3, 3 },
                new int [4] { 3, 3, 3, 3 }
                };
                // 各Cube边的每个门相对（反方向边）的第一个门序号
                int[][] faceSideStart = new int[9][]
                {
                new int [4] { 0, 1, 2, 3 },
                new int [4] { 0, 3, 6, 9 },
                new int [4] { 0, 3, 6, 9 },
                new int [4] { 0, 3, 4, 7 },
                new int [4] { 0, 1, 4, 5 },
                new int [4] { 0, 3, 4, 7 },
                new int [4] { 0, 1, 4, 5 },
                new int [4] { 0, 3, 6, 9 },
                new int [4] { 0, 3, 6, 9 }
                };
                // 门反向方向序号
                int[][] oppositesIndex = new int[9][]
                {
                new int [12] { 2, 3, 0, 1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int [12] { 2, 2, 2, 3, 3, 3, 0, 0, 0, 1, 1, 1 },
                new int [12] { 2, 2, 2, 3, 3, 3, 0, 0, 0, 1, 1, 1 },
                new int [12] { 2, 2, 2, 3, 0, 0, 0, 1, -1, -1, -1, -1 },
                new int [12] { 2, 3, 3, 3, 0, 1, 1, 1, -1, -1, -1, -1 },
                new int [12] { 2, 2, 2, 3, 0, 0, 0, 1, -1, -1, -1, -1 },
                new int [12] { 2, 3, 3, 3, 0, 1, 1, 1, -1, -1, -1, -1 },
                new int [12] { 2, 2, 2, 3, 3, 3, 0, 0, 0, 1, 1, 1 },
                new int [12] { 2, 2, 2, 3, 3, 3, 0, 0, 0, 1, 1, 1 }
                };
                // 门记录排列序号（不满12）到全门类型序号（12个）转换
                int[][] translate = new int[9][]
                {
                new int [12] { 1, 4, 7, 10, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int [12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                new int [12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                new int [12] { 0, 1, 2, 4, 6, 7, 8, 10, -1, -1, -1, -1 },
                new int [12] { 1, 3, 4, 5, 7, 9, 10, 11, -1, -1, -1, -1 },
                new int [12] { 0, 1, 2, 4, 6, 7, 8, 10, -1, -1, -1, -1 },
                new int [12] { 1, 3, 4, 5, 7, 9, 10, 11, -1, -1, -1, -1 },
                new int [12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                new int [12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }
                };
                // 全门类型序号（12个）到门记录排列序号（不满12）转换
                int[][] untranslate = new int[9][]
                {
                new int [12] { -1, 0, -1, -1, 1, -1, -1, 2, -1, -1, 3, -1 },
                new int [12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                new int [12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                new int [12] { 0, 1, 2, -1, 3, -1, 4, 5, 6, -1, 7, -1 },
                new int [12] { -1, 0, -1, 1, 2, 3, -1, 4, -1, 5, 6, 7 },
                new int [12] { 0, 1, 2, -1, 3, -1, 4, 5, 6, -1, 7, -1  },
                new int [12] { -1, 0, -1, 1, 2, 3, -1, 4, -1, 5, 6, 7 },
                new int [12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                new int [12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }
                };
                // 各个类型门对应的边类型：0:16,1-3:32,4-6:64
                int[][] sideType = new int[9][]
                {
                new int [12] { 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int [12] { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 },
                new int [12] { 4, 5, 6, 4, 5, 6, 4, 5, 6, 4, 5, 6 },
                new int [12] { 1, 2, 3, 0, 1, 2, 3, 0, -1, -1, -1, -1 },
                new int [12] { 0, 1, 2, 3, 0, 1, 2, 3, -1, -1, -1, -1 },
                new int [12] { 4, 5, 6, 0, 4, 5, 6, 0, -1, -1, -1, -1 },
                new int [12] { 0, 4, 5, 6, 0, 4, 5, 6, -1, -1, -1, -1 },
                new int [12] { 4, 5, 6, 1, 2, 3, 4, 5, 6, 1, 2, 3 },
                new int [12] { 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 6 }
                };
                // 两门连接类型通行关系
                string[][] linkAble = new string[7][]
                {
                new string [7] { "true", "true", "false", "true", "false", "false", "false" },
                new string [7] { "true", "true", "false", "true", "false", "false", "false" },
                new string [7] { "false", "false", "true", "false", "true", "true", "true" },
                new string [7] { "true", "true", "false", "true", "false", "false", "false" },
                new string [7] { "false", "false", "true", "false", "true", "true", "true" },
                new string [7] { "false", "false", "true", "false", "true", "true", "true" },
                new string [7] { "false", "false", "true", "false", "true", "true", "true" },
                };
                // 12个门坐标偏移倍率
                double[][] rate = new double[12][]
                {
                new double [2] { -0.5, -1 },
                new double [2] { 0, -1 },
                new double [2] { 0.5, -1 },
                new double [2] { 1, -0.5 },
                new double [2] { 1, 0 },
                new double [2] { 1, 0.5 },
                new double [2] { 0.5, 1 },
                new double [2] { 0, 1 },
                new double [2] { -0.5, 1 },
                new double [2] { -1, 0.5 },
                new double [2] { -1, 0 },
                new double [2] { -1, -0.5 }
                };
                // 各Cube类型边长
                double[][] side = new double[9][]
                {
                new double [2] { 16, 16 },
                new double [2] { 32, 32 },
                new double [2] { 64, 64 },
                new double [2] { 32, 16 },
                new double [2] { 16, 32 },
                new double [2] { 64, 16 },
                new double [2] { 16, 64 },
                new double [2] { 64, 32 },
                new double [2] { 32, 64 }
                };
                // 各方向正负值
                double[][] valueFace = new double[4][]
                {
                new double [2] { -1, -1 },
                new double [2] { 1, -1 },
                new double [2] { 1, 1 },
                new double [2] { -1, 1 }
                };
                // 各方向正负值
                double[][] sideOrder = new double[6][]
                {
                new double [3] { 0, 1, 2 },
                new double [3] { 0, 2, 1 },
                new double [3] { 1, 0, 2 },
                new double [3] { 1, 2, 0 },
                new double [3] { 2, 1, 0 },
                new double [3] { 2, 0, 1 },
                };

                codes += "void LibEditor_FUNC_RWS_InitialFunction()\r\n{\r\n";

                codes += "// Link relation of Gate side type \r\n";

                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        codes += "libEditor_GV_RWS_ConcentrateModeLinkRelation[" + (i).ToString() + "][" + j.ToString() + "] = " + linkAble[i][j] + ";\r\n";
                    }
                }

                codes += "\r\n// Check for this cube in  concentrate mode \r\n";

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < typeGateCount[i]; j++)
                    {
                        codes += "libEditor_GV_RWS_ConcentrateModeCubeCheckValue[" + (i).ToString() + "][" + j.ToString() + "] = " + (sideType[i][j]).ToString() + ";\r\n";
                    }
                }

                codes += "\r\n// Link Gate priority order\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomValue[0] = -1;\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomValue[1] = 0;\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomValue[2] = 2;\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomValue[3] = 8;\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomPriorityOrder[0][0] = 0;\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomPriorityOrder[1][0] = 0;\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomPriorityOrder[1][1] = 1;\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomPriorityOrder[2][0] = 1;\r\n";
                codes += "libEditor_GV_RWS_CubeSideRandomPriorityOrder[2][1] = 0;\r\n";
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        codes += "libEditor_GV_RWS_CubeSideRandomPriorityOrder[" + (i + 3).ToString() + "][" + j.ToString() + "] = " + (sideOrder[i][j]).ToString() + ";\r\n";
                    }
                }

                codes += "\r\n// Set Side Length\r\n";

                for (int i = 0; i < 9; i++)
                {
                    codes += "libEditor_GV_RWS_CubeSideLength[" + i.ToString() + "][0] = " + (side[i][0]).ToString() + ";\r\n";
                    codes += "libEditor_GV_RWS_CubeSideLength[" + i.ToString() + "][1] = " + (side[i][1]).ToString() + ";\r\n";
                }

                codes += "\r\n// From Cube record Gate index to 12 Gate index\r\n";

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        codes += "libEditor_GV_RWS_CubeInTypeGateIndexToIndex[" + i.ToString() + "][" + j.ToString() + "] = " + (untranslate[i][j]).ToString() + ";\r\n";
                    }
                }

                codes += "\r\n// Count of Gate in Cube type\r\n";

                for (int i = 0; i < 9; i++)
                {
                    codes += "libEditor_GV_RWS_GateCountPerCubeType[" + i.ToString() + "] = " + (typeGateCount[i]).ToString() + ";\r\n";
                }

                codes += "\r\n// Count of Gate in Cube Side\r\n";

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        codes += "libEditor_GV_RWS_GateCountPerSide[" + i.ToString() + "][" + j.ToString() + "] = " + (sideGateCount[i][j]).ToString() + ";\r\n";
                    }
                }

                codes += "\r\n// The first index of Gate of Cube side\r\n";

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (j < typeGateCount[i])
                        {
                            codes += "libEditor_GV_RWS_LinkGateSideStartType[" + i.ToString() + "][" + j.ToString() + "] = " + (faceSideStart[i][j]).ToString() + ";\r\n";
                        }
                        else
                        {
                            codes += "libEditor_GV_RWS_LinkGateSideStartType[" + i.ToString() + "][" + j.ToString() + "] = -1;\r\n";
                        }
                    }
                }

                codes += "\r\n// The opposite Side index of Gate belong, 0:Bottom, 1:Right, 2:Up, 3:Left\r\n";

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (j < typeGateCount[i])
                        {
                            codes += "libEditor_GV_RWS_OppositeSideIndexForGate[" + i.ToString() + "][" + j.ToString() + "] = " + (oppositesIndex[i][j]).ToString() + ";\r\n";
                        }
                        else
                        {
                            break;
                        }

                    }
                }

                codes += "\r\n// Set Vertex Moving From Center\r\n";

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        codes += "libEditor_GV_RWS_CubeVertexMoving[" + i.ToString() + "][" + j.ToString() + "][0] = " + (side[i][0] / 2 * valueFace[j][0]).ToString() + ";\r\n";
                        codes += "libEditor_GV_RWS_CubeVertexMoving[" + i.ToString() + "][" + j.ToString() + "][1] = " + (side[i][1] / 2 * valueFace[j][1]).ToString() + ";\r\n";
                    }
                }

                codes += "\r\n// Set Gate Moving From Center\r\n";

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (j < typeGateCount[i])
                        {
                            codes += "libEditor_GV_RWS_CubeGateMoving[" + i.ToString() + "][" + j.ToString() + "][0] = " + (side[i][0] * rate[translate[i][j]][0] / 2).ToString() + ";\r\n";
                            codes += "libEditor_GV_RWS_CubeGateMoving[" + i.ToString() + "][" + j.ToString() + "][1] = " + (side[i][1] * rate[translate[i][j]][1] / 2).ToString() + ";\r\n";
                        }
                        else
                        {
                            break;
                        }

                    }
                }

                codes += "\r\n// Set Gate Test Moving From Center\r\n";

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (j < typeGateCount[i])
                        {
                            switch (oppositesIndex[i][j])
                            {
                                case 2:
                                    codes += "libEditor_GV_RWS_CubeGateTestMoving[" + i.ToString() + "][" + j.ToString() + "][0] = " + (side[i][0] * rate[translate[i][j]][0] / 2).ToString() + ";\r\n";
                                    codes += "libEditor_GV_RWS_CubeGateTestMoving[" + i.ToString() + "][" + j.ToString() + "][1] = " + (side[i][1] * rate[translate[i][j]][1] / 2 - 15).ToString() + ";\r\n";
                                    break;
                                case 3:
                                    codes += "libEditor_GV_RWS_CubeGateTestMoving[" + i.ToString() + "][" + j.ToString() + "][0] = " + (side[i][0] * rate[translate[i][j]][0] / 2 + 15).ToString() + ";\r\n";
                                    codes += "libEditor_GV_RWS_CubeGateTestMoving[" + i.ToString() + "][" + j.ToString() + "][1] = " + (side[i][1] * rate[translate[i][j]][1] / 2).ToString() + ";\r\n";
                                    break;
                                case 0:
                                    codes += "libEditor_GV_RWS_CubeGateTestMoving[" + i.ToString() + "][" + j.ToString() + "][0] = " + (side[i][0] * rate[translate[i][j]][0] / 2).ToString() + ";\r\n";
                                    codes += "libEditor_GV_RWS_CubeGateTestMoving[" + i.ToString() + "][" + j.ToString() + "][1] = " + (side[i][1] * rate[translate[i][j]][1] / 2 + 15).ToString() + ";\r\n";
                                    break;
                                case 1:
                                    codes += "libEditor_GV_RWS_CubeGateTestMoving[" + i.ToString() + "][" + j.ToString() + "][0] = " + (side[i][0] * rate[translate[i][j]][0] / 2 - 15).ToString() + ";\r\n";
                                    codes += "libEditor_GV_RWS_CubeGateTestMoving[" + i.ToString() + "][" + j.ToString() + "][1] = " + (side[i][1] * rate[translate[i][j]][1] / 2).ToString() + ";\r\n";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                codes += "\r\n// Set Cube overlap test\r\n";
                for (int i = 0; i < 9; i++)
                {
                    int l = 0;
                    for (double j = (side[i][0] % 14 - side[i][0]) / 2; j < side[i][0] / 2; j += 14)
                    {
                        for (double k = (side[i][1] % 14 - side[i][1]) / 2; k < side[i][1] / 2; k += 14)
                        {
                            codes += "libEditor_GV_RWS_CubeOverlapTestMoving[" + i.ToString() + "][" + l.ToString() + "][0] = " + j.ToString() + ";\r\n";
                            codes += "libEditor_GV_RWS_CubeOverlapTestMoving[" + i.ToString() + "][" + l.ToString() + "][1] = " + k.ToString() + ";\r\n";
                            l++;
                        }
                    }
                    codes += "libEditor_GV_RWS_CubeOverlapTestCount[" + i.ToString() + "] = " + l.ToString() + ";\r\n";
                }
                #endregion
            }
            CodeShowing.Text = codes;
            Clipboard.SetText(codes);
        }
    }
}
