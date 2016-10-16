using System;
using System.Collections.Generic;
using System.Text;

namespace 频谱仪E4407B
{
    /// <summary>
    /// 峰值搜索结果实体类
    /// </summary>
    public class peakSearchResult
    {
        public double markerFreq { get; set; }  //频率
        public double markerAmpl { get; set; } //幅度
    }

    /// <summary>
    /// 频谱仪E4407B接口规范
    /// </summary>
    public interface ISpectrumAnalyzerE4407B
    {
        //属性
        bool isConnected { get; set; }                   //是否已连接
        //方法
        void connect(string resourceName);       //连接
        void disConnect();                                    //断开
        string GetInstrumentID();                         //获取仪器的ID字符串
        void reset();                                              //频谱仪复位
        void save();                                               //保存当前图像

        void set起始终止频率(double startFreq,double endFreq);    //起始终止频率，单位MHz
        void set中心频率(double centerFreq);      // 设置中心频率，单位MHz
        void set频率范围(double freqSpan);         //设置频率范围，单位MHz
        void set起始频率(double startFreq);         //起始频率，单位MHz
        void set终止频率(double endFreq);          //终止频率，单位MHz
        void set参考电平(double refLevel);           //参考电平，单位dBm
        void setScaleYPDiv(double scaleY);         //Y轴每格电平，单位dBm
        void set分辨带宽(double bwidRes);          //分辨带宽，单位KHz
        void set视频带宽(double bwidVid);          //视频带宽，单位KHz
        void setVbwRbw(double vbwRbw);         //设置Vbw/Rbw ？
        peakSearchResult peakSearch(double peakExcursion);  //峰值搜索 ，参数？
        peakSearchResult nextPeek(double peakExcursion);      //下一个峰值
		
		void get功率();
    }
}
