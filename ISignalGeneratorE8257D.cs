using System;
namespace 信号源E8257D
{
    /// <summary>
    /// 微波信号源E8257D接口规范
    /// </summary>
    public interface ISignalGeneratorE8257D
    {
        //属性
        bool isConnected { get; set; }           //是否已连接
        //方法
        void connect(string resourceName);       //连接
        void disConnect();                       //断开
        string GetInstrumentID();                //获取仪器ID字符串
        void setFrequency(double freq);          //设置频率
        void setMod(bool isOpen);                //设置MOD
        void setPower(double pow);               //设置功率
        void setPulm(bool isOpen);               //设置PULM
        void setPulsePeriod(double pulsePeriod); //设置脉冲重复周期
        void setPulseWidth(double pulseWidth);   //设置脉宽
        void setRFout(bool isOpen);              //设置RF
    }
}
