/******************************************************************************
*                                                                             *
*   PROJECT : Eos Digital camera Software Development Kit EDSDK               *
*                                                                             *
*   Description: This is the Sample code to show the usage of EDSDK.          *
*                                                                             *
*                                                                             *
*******************************************************************************
*                                                                             *
*   Written and developed by Canon Inc.                                       *
*   Copyright Canon Inc. 2018 All Rights Reserved                             *
*                                                                             *
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using LightAPI;
using System.Threading;
using System.Xml;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Xml.Linq;

namespace CameraControl
{
    public partial class RemoteCapture : Form
    {

        private CameraController _controller = null;
        private ActionSource _actionSource = null;
        private List<IObserver> _observerList = new List<IObserver>();
        string COM = "COM4";
        Light lightInstance;
        int Frequency = 5;
        string SavePath = "";
        Dictionary<int, SettingData> TotalSetting = new Dictionary<int, SettingData>();
        Thread checkThread = null;

        public class SettingData
        {
            public int LV { get; set; }
            public int LightValue { get; set; }
            public string TV { get; set; }
            public string ISO { get; set; }
            public string AV { get; set; }
            public string Flash { get; set; }

        }
        public RemoteCapture(ref CameraController controller, ref ActionSource actionSource)
        {
            InitializeComponent();

            _controller = controller;

            _actionSource = actionSource;

            CameraEvent e;

            _observerList.Add(av1);
            _observerList.Add(tv1);
            _observerList.Add(iso1);
            System.Threading.Thread.Sleep(1000);

            _observerList.ForEach(observer => _controller.GetModel().Add(ref observer));

            e = new CameraEvent(CameraEvent.Type.PROPERTY_DESC_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_AEModeSelect);
            _controller.GetModel().NotifyObservers(e);

            av1.SetActionSource(ref _actionSource);
            e = new CameraEvent(CameraEvent.Type.PROPERTY_DESC_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_Av);
            _controller.GetModel().NotifyObservers(e);

            
            tv1.SetActionSource(ref _actionSource);
            e = new CameraEvent(CameraEvent.Type.PROPERTY_DESC_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_Tv);
            _controller.GetModel().NotifyObservers(e);


            iso1.SetActionSource(ref _actionSource);
            e = new CameraEvent(CameraEvent.Type.PROPERTY_DESC_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_ISOSpeed);
            _controller.GetModel().NotifyObservers(e);


            e = new CameraEvent(CameraEvent.Type.PROPERTY_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_AFMode);
            _controller.GetModel().NotifyObservers(e);

            e = new CameraEvent(CameraEvent.Type.PROPERTY_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_Evf_AFMode);
            _controller.GetModel().NotifyObservers(e);

            e = new CameraEvent(CameraEvent.Type.PROPERTY_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_AvailableShots);
            _controller.GetModel().NotifyObservers(e);

            e = new CameraEvent(CameraEvent.Type.PROPERTY_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_BatteryLevel);
            _controller.GetModel().NotifyObservers(e);

            e = new CameraEvent(CameraEvent.Type.PROPERTY_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_TempStatus);
            _controller.GetModel().NotifyObservers(e);

            e = new CameraEvent(CameraEvent.Type.PROPERTY_CHANGED, (IntPtr)EDSDKLib.EDSDK.PropID_FixedMovie);
            _controller.GetModel().NotifyObservers(e);

            if (!_controller.GetModel().isTypeDS)
            {
                _actionSource.FireEvent(ActionEvent.Command.REMOTESHOOTING_START, IntPtr.Zero);
            }
            ReadSetting();

            lightInstance = new Light("COM4", 9600, 8);
            lightInstance.ReceiveData += Lt_ReceiveData;

            //初始化測光執行續
            checkThread = new Thread(GetLightValue);
            checkThread.Start();
        }

        private void GetLightValue()
        {
            try
            {
                while (true)
                {
                    lightInstance.SendData(Encoding.ASCII.GetBytes("m@G02#"));
                    Thread.Sleep(Frequency * 1000);
                }
            }
            catch (Exception ie)
            {
            }
        }

        public void ReadSetting()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Setting.xml");//載入xml檔
                var settingNode = xmlDoc.SelectSingleNode("Setting");
                var frequency = ((XmlElement)settingNode).GetAttribute("Frequency");
                COM = ((XmlElement)settingNode).GetAttribute("COM");
                if (lightInstance != null)
                    lightInstance.SetCOM(COM);
                Frequency = int.Parse(frequency);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"讀取設定發生例外 : {ex.Message}\r\n{ex.StackTrace}");
            }

        }

        [STAThread]
        private void Lt_ReceiveData(object sender, EventArgs e)
        {
            try
            {
                var light = (sender as Light).LightValue.ToString();

                Action<string> actionDelegate = (x) => { this.textBox1.Text = light; };
                this.textBox1.Invoke(actionDelegate, light);
                //依照測光值設定相機
                if (checkBox1.Checked) 
                {
                    SetCamera((sender as Light).LightValue);
                }
               
                Thread.Sleep(Frequency * 1000);

            }
            catch (Exception ie)
            {
                MessageBox.Show($"設定介面時發生例外 {ie.Message}");
            }
        }
        string Flash = "ON";
        string ISO = "";
        string Light = "";
        string Shutter = "";
        string Aperture = "";

        /// <summary>
        /// 依照測光值設定相機
        /// </summary>
        /// <param name="lightValue"></param>
        [STAThread]
        public void SetCamera(int lightValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load("Setting.xml");//載入xml檔
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"讀取設定失敗 {ex.Message}，重新產生預設設定");
                CreatDefaultSetting();
                return;
            }


            var root = xmlDoc.SelectSingleNode("Setting");
            SavePath = ((XmlElement)root).GetAttribute("Path");

            var data = xmlDoc.SelectNodes("Setting/LV");
            TotalSetting.Clear();
            foreach (var item in data)
            {
                var lv = ((XmlElement)item).GetAttribute("LV");
                var value = ((XmlElement)item).GetAttribute("Value");
                var tv = ((XmlElement)item).GetAttribute("TV");
                var iso = ((XmlElement)item).GetAttribute("ISO");
                var av = ((XmlElement)item).GetAttribute("AV");
                var flash = ((XmlElement)item).GetAttribute("Flash");
                TotalSetting.Add(int.Parse(value), new SettingData()
                {
                    AV = av,
                    Flash = flash,
                    ISO = iso,
                    LightValue = int.Parse(value),
                    LV = int.Parse(lv),
                    TV = tv
                });
            }
            if (checkBox1.Checked) 
            {
                var currentHr = int.Parse(DateTime.Now.ToString("HH"));
                //1800~0600套用level1設定
                if ((currentHr > 17 && currentHr < 25) || (currentHr <= 6))
                {

                    var currentSetting = TotalSetting.Where(x => x.Value.LV == 1).FirstOrDefault().Value;
                    Flash = currentSetting.Flash;
                    ISO = currentSetting.ISO;
                    Light = lightValue.ToString();
                    Shutter = currentSetting.TV;
                    Aperture = currentSetting.AV;
                    label8.InvokeIfRequired(() =>
                    {
                        label8.Text = "目前時間介於18:00~06:00套用level1設定";
                    });
                }
                else
                {
                    label8.InvokeIfRequired(() =>
                    {
                        label8.Text = "";
                    });
                    //如果設定資料中有完全符合的測光值設定
                    if (TotalSetting.ContainsKey(lightValue))
                    {
                        var dt = TotalSetting[lightValue];
                        Flash = dt.Flash;
                        ISO = dt.ISO;
                        Light = lightValue.ToString();
                        Shutter = dt.TV;
                        Aperture = dt.AV;
                    }
                    //沒完全符合，抓最接近
                    else
                    {
                        var minLight = TotalSetting.Where(x => x.Key > lightValue).Min(x => x.Key);
                        var dt = TotalSetting[minLight];
                        Flash = dt.Flash;
                        ISO = dt.ISO;
                        Light = lightValue.ToString();
                        Shutter = dt.TV;
                        Aperture = dt.AV;
                    }
                }
            }
            else 
            {
                label8.InvokeIfRequired(() =>
                {
                    label8.Text = "";
                });
                //如果設定資料中有完全符合的測光值設定
                if (TotalSetting.ContainsKey(lightValue))
                {
                    var dt = TotalSetting[lightValue];
                    Flash = dt.Flash;
                    ISO = dt.ISO;
                    Light = lightValue.ToString();
                    Shutter = dt.TV;
                    Aperture = dt.AV;
                }
                //沒完全符合，抓最接近
                else
                {
                    var minLight = TotalSetting.Where(x => x.Key > lightValue).Min(x => x.Key);
                    var dt = TotalSetting[minLight];
                    Flash = dt.Flash;
                    ISO = dt.ISO;
                    Light = lightValue.ToString();
                    Shutter = dt.TV;
                    Aperture = dt.AV;
                }

            }



            //Set UI Info
            label9.InvokeIfRequired(() =>
            {
                label9.Text = $"測光值: {lightValue}\rAV : {Aperture}\rISO : {ISO}\rTV : {Shutter}\rFlash : {Flash}";
            });
            //Set Camera
            SettingAV(Aperture);
            SettingISO(ISO);
            SettingTV(Shutter);

            if (Flash.Equals("ON")) 
            {
                label6.InvokeIfRequired(() =>
                {
                    label6.Text = "ON";
                });
                SettingRelayFlash(true);
            }
            else 
            {
                SettingRelayFlash(false);
                label6.InvokeIfRequired(() =>
                {
                    label6.Text = "OFF";
                });
            }

        }

        /// <summary>
        /// 設定測光板閃光機是否啟動
        /// </summary>
        /// <param name="isTurnOnFlash"></param>
        public void SettingRelayFlash(bool isTurnOnFlash)
        {
            if (isTurnOnFlash)
                lightInstance.SendData(Encoding.ASCII.GetBytes("m@M01,21#"));
            else
                lightInstance.SendData(Encoding.ASCII.GetBytes("m@M01,20#"));
        }

        /// <summary>
        /// 設定相機AV
        /// </summary>
        /// <param name="value"></param>
        public void SettingAV(string value)
        {
            var decimalAV = double.Parse(value);
            try
            {
                int index = 0;
                switch (decimalAV)
                {
                    case 5.0:
                        index = 0;
                        break;
                    case 5.6:
                        index = 1;
                        break;
                    case 6.3:
                        index = 2;
                        break;
                    case 8.0:
                        index = 4;
                        break;
                    case 9.0:
                        index = 5;
                        break;
                    default:
                        index = 999;
                        break;
                }
                if (index == 999) 
                {
                    MessageBox.Show($"設定相機光圈(AV)發生錯誤:\r\n設定值{value} 找不到對應項目");
                }
                else 
                {
                    av1.InvokeIfRequired(() =>
                    {
                        av1.SelectedIndex = index;
                        av1.Set(index);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"設定相機光圈發生例外\r\n{ex.Message}\r\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// 設定相機TV
        /// </summary>
        /// <param name="value"></param>
        public void SettingTV(string value)
        {
            try
            {
                int index = 0;
                switch (value)
                {
                    case "1/160":
                        index = 38;
                        break;
                    case "1/200":
                        index = 39;
                        break;
                    case "1/250":
                        index = 40;
                        break;
                    case "1/320":
                        index = 41;
                        break;
                    case "1/400":
                        index = 42;
                        break;
                    case "1/500":
                        index = 43;
                        break;
                    case "1/640":
                        index = 44;
                        break;
                    case "1/800":
                        index = 45;
                        break;
                    case "1/1000":
                        index = 46;
                        break;
                    case "1/1250":
                        index = 47;
                        break;
                    case "1/1600":
                        index = 48;
                        break;
                    default:
                        index = 999;
                        break;
                }

                if (index > 100)
                {
                    MessageBox.Show($"設定相機快門(TV)發生錯誤\r\nTV設定值{value}找不到對應值");
                }
                else
                {
                    tv1.InvokeIfRequired(() =>
                    {
                        tv1.SelectedIndex = index;
                        tv1.Set(index);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"設定相機快門發生例外\r\n{ex.Message}\r\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// 產生預設設定檔
        /// </summary>
        public void CreatDefaultSetting()
        {
            XElement xElement = new XElement(
                new XElement("Setting",
                new XAttribute("COM", "COM4"),
                new XAttribute("Frequency", "180"),
                    new XAttribute("Path", "C:\\"),
                        new XElement("LV",
                            new XAttribute("LV", "1"),
                            new XAttribute("Value", "3"),
                            new XAttribute("TV", "1/320"),
                            new XAttribute("ISO", "ISO 800"),
                            new XAttribute("AV", "4"),
                            new XAttribute("Flash", "ON")
                            ),
                        new XElement("LV",
                            new XAttribute("LV", "2"),
                            new XAttribute("Value", "20"),
                            new XAttribute("TV", "1/800"),
                            new XAttribute("ISO", "ISO 800"),
                            new XAttribute("AV", "5.6"),
                            new XAttribute("Flash", "ON")
                            ),
                        new XElement("LV",
                            new XAttribute("LV", "3"),
                            new XAttribute("Value", "30"),
                            new XAttribute("TV", "1/1000"),
                            new XAttribute("ISO", "ISO 800"),
                            new XAttribute("AV", "5.6"),
                            new XAttribute("Flash", "OFF")
                            ),
                        new XElement("LV",
                            new XAttribute("LV", "4"),
                            new XAttribute("Value", "40"),
                            new XAttribute("TV", "1/1000"),
                            new XAttribute("ISO", "ISO 800"),
                            new XAttribute("AV", "5.6"),
                            new XAttribute("Flash", "OFF")
                            ),
                        new XElement("LV",
                            new XAttribute("LV", "5"),
                            new XAttribute("Value", "70"),
                            new XAttribute("TV", "1/1000"),
                            new XAttribute("ISO", "ISO 400"),
                            new XAttribute("AV", "5.6"),
                            new XAttribute("Flash", "OFF")
                            ),
                        new XElement("LV",
                            new XAttribute("LV", "6"),
                            new XAttribute("Value", "100"),
                            new XAttribute("TV", "1/1000"),
                            new XAttribute("ISO", "ISO 400"),
                            new XAttribute("AV", "6.3"),
                            new XAttribute("Flash", "OFF")
                            ),
                        new XElement("LV",
                            new XAttribute("LV", "7"),
                            new XAttribute("Value", "120"),
                            new XAttribute("TV", "1/1000"),
                            new XAttribute("ISO", "ISO 200"),
                            new XAttribute("AV", "7.1"),
                            new XAttribute("Flash", "OFF")
                            ),
                        new XElement("LV",
                            new XAttribute("LV", "8"),
                            new XAttribute("Value", "140"),
                            new XAttribute("TV", "1/1000"),
                            new XAttribute("ISO", "ISO 400"),
                            new XAttribute("AV", "8"),
                            new XAttribute("Flash", "OFF")
                            ),
                         new XElement("LV",
                            new XAttribute("LV", "9"),
                            new XAttribute("Value", "160"),
                            new XAttribute("TV", "1/1000"),
                            new XAttribute("ISO", "ISO 400"),
                            new XAttribute("AV", "9"),
                            new XAttribute("Flash", "OFF")
                            ),
                          new XElement("LV",
                            new XAttribute("LV", "10"),
                            new XAttribute("Value", "255"),
                            new XAttribute("TV", "1/1000"),
                            new XAttribute("ISO", "ISO 800"),
                            new XAttribute("AV", "9"),
                            new XAttribute("Flash", "ON")
                            )
                        )
                );

            //需要指定編碼格式，否則在讀取時會拋：根級別上的資料無效。 第 1 行 位置 1異常
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(System.Environment.CurrentDirectory + "\\setting.xml", settings);
            xElement.Save(xw);
            //寫入檔案
            xw.Flush();
            xw.Close();
        }

        /// <summary>
        /// 設定相機ISO
        /// </summary>
        /// <param name="value"></param>
        public void SettingISO(string value)
        {
            try
            {
                int index = 99990;
                switch (value)
                {
                    case "ISO 100":
                        index = 1;
                        break;
                    case "ISO 200":
                        index = 4;
                        break;
                    case "ISO 400":
                        index = 7;
                        break;
                    case "ISO 800":
                        index = 10;
                        break;
                    case "ISO 1600":
                        index = 13;
                        break;
                    default:
                        index = 9999;
                        break;
                }
                if (index > 100) 
                {
                    MessageBox.Show($"設定相機ISO發生錯誤\r\nISO設定值{value}找不到對應值");
                }
                else 
                {
                    iso1.InvokeIfRequired(() =>
                    {
                        iso1.SelectedIndex = index;
                        iso1.Set(index);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"設定相機ISO發生例外\r\n{ex.Message}\r\n{ex.StackTrace}");
            }
        }
        
        private void RemoteCapture_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
            _actionSource.FireEvent(ActionEvent.Command.END_ROLLPITCH, IntPtr.Zero);
            _actionSource.FireEvent(ActionEvent.Command.END_EVF, IntPtr.Zero);
            if (!_controller.GetModel().isTypeDS)
            {
                _actionSource.FireEvent(ActionEvent.Command.REMOTESHOOTING_STOP, IntPtr.Zero);
            }
            _actionSource.FireEvent(ActionEvent.Command.PRESS_OFF, IntPtr.Zero);
            _actionSource.FireEvent(ActionEvent.Command.EVF_AF_OFF, IntPtr.Zero);
            _observerList.ForEach(observer => _controller.GetModel().Remove(ref observer));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
                SetCamera(int.Parse(textBox2.Text));
        }
    }
}
