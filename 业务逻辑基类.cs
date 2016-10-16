using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 测控台_485卡;
using 频谱仪E4407B;
using 通用工具类库;
using 信号源E8257D;

namespace 业务逻辑
{
    /// <summary>
    /// 业务逻辑类的抽象基类，表示业务逻辑共有的属性、行为
    /// </summary>
    public abstract class 业务逻辑基类
    {
        #region 类属性（公共权限）
        /// <summary>
        /// 类属性：表示是否有测试项正在测试中，true表示是，false表示不是
        /// </summary>
        private static bool is测试中 = false;  //默认没有测试项正在进行测试
        #endregion

        #region 字段
        //注：把字段访问级别设置为protect，是因为该字段只允许本类及其子类访问，在其他类中不可以通过本类对象或者子类对象访问
        //设备引用（注：这里把整个项目所有设备都囊扩了，以便具体测试类使用，具体测试类使用哪个，就在“资源验证方法”中查看所需设备是否已连接
        protected 测控台 obj测控台 = null;
        protected ISpectrumAnalyzerE4407B obj频谱仪 = null;
        protected ISignalGeneratorE8257D obj信号源1 = null;
        protected ISignalGeneratorE8257D obj信号源2 = null;
        //线程引用
        protected Thread runThread = null;         
        //线程退出标志量（内置bool类型）
        protected bool is终止测试线程;
        //保存文件的路径
        protected string 文件路径 = "..\\..\\..\\..\\";
        #endregion

        #region 属性
        //业务名称
        public string 业务名称 { get; set; }
        public 业务ID 业务ID { get; set; }
        //事件
        public event EventHandler 测前配置;
        public event EventHandler 数据发布;
        public event EventHandler 测试完毕;
        #endregion

        #region 构造器
        public 业务逻辑基类(string 业务名称 , 业务ID 业务ID)
        {
            //业务名称
            this.业务名称 = 业务名称;
            //业务ID
            this.业务ID = 业务ID;

            //设备实例化（所有设备）
            obj测控台 = 测控台.getInstance();
            obj频谱仪 = SpectrumAnalyzerE4407B.getInstance();
            obj信号源1 = SignalGeneratorE8257D.getInstance(1);
            obj信号源2 = SignalGeneratorE8257D.getInstance(2);
            //线程退出标志量初始化
            is终止测试线程 = false;
        }
        #endregion

        #region 方法

        #region 虚方法（事件方法）：子类可覆写重新定义
        /// <summary>
        /// 测试入口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void on测前配置(object sender, EventArgs e)
        {
            if (测前配置 != null)
            {
                测前配置(sender, e);

                is测试中 = true;  //在测试入口，把“is测试中”标志位置true
            }
        }
        /// <summary>
        /// 测试数据发布口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void on数据发布(object sender, EventArgs e)
        {
            if (数据发布 != null)
            {
                数据发布(sender, e);
            }
        }
        /// <summary>
        /// 测试出口：注意（任何情况下，测试只能从本出口结束，以便在出口处进行“善后处理”）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void on测试完毕(object sender, EventArgs e)
        {
            if (测试完毕 != null)
            {
                测试完毕(sender, e);

                is测试中 = false;  //在测试出口，把“is测试中”标志位置false
            }
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 内部抽象方法：用于验证该测试所用的资源是否都已连接
        /// </summary>
        /// <returns></returns>
        protected abstract bool 资源连接验证();
        /// <summary>
        /// 测试的主体部分
        /// </summary>
        protected abstract void 测试方法体正文();
        /// <summary>
        /// 测试完毕或测试遇到异常后，退出测试前所执行的操作
        /// </summary>
        protected abstract void 测试方法体结束处理();
        #endregion

        #region 内部方法
        private void 测试方法体()
        {
            try
            {
                测试方法体正文();
                if (is终止测试线程)
                {
                    //事件：手动终止测试
                    on测试完毕(this, new 手动结束EventArgs());
                }
                else
                {
                    //事件：测试正常结束
                    on测试完毕(this, new 正常结束EventArgs());
                }
            }
            catch (Exception e)
            {
                on测试完毕(this, new 异常结束EventArgs(e));
            }
            finally
            {
                测试方法体结束处理();
            }
        }
        #endregion

        #region 接口方法

        /// <summary>
        /// 检查是否有测试项正在测试
        /// </summary>
        /// <returns></returns>
        public bool 是否正在测试()
        {
            if (is测试中)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 启动测试流程
        /// </summary>
        /// <param name="is开辟线程">true：业务在子线程中执行，false：业务在主线程中执行</param>
        public void Run(bool is开辟线程 = true)
        {
            //检查其他测试项是否正在测试
            if (is测试中)
            {
                MessageBox.Show("某测试项正在测试中，如果要进行测试，请先终止其他测试项！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //else
            //{
            //    is测试中 = true;  //开始本项测试前，先把“is测试中”标志位置true
            //}

            //资源连接验证
            if (!资源连接验证())
            {
                //事件：测试完毕
                on测试完毕(this, null);
                return;   //退出本测试项
            }

            //事件：测前配置
            this.on测前配置(this, null);

            if (is开辟线程)
            {
                //开启本测试项线程
                this.is终止测试线程 = false;
                runThread = new Thread(new ThreadStart(测试方法体));
                Control.CheckForIllegalCrossThreadCalls = false;
                runThread.Start();
            }
            else
            {
                测试方法体();
            }
        }
        /// <summary>
        /// 终止测试流程
        /// </summary>
        public void stop()
        {
            if (runThread != null && runThread.ThreadState != ThreadState.Suspended)
            {
                this.is终止测试线程 = true;
            }
        }
        /// <summary>
        /// 暂停测试流程
        /// </summary>
        public void pause()
        {
            if (runThread != null && runThread.ThreadState != ThreadState.Aborted)
            {
                runThread.Suspend();
            }
        }
        /// <summary>
        /// 继续测试流程
        /// </summary>
        public void go()
        {
            if (runThread != null && runThread.ThreadState != ThreadState.Aborted)
            {
                runThread.Resume();
            }
        }
        #endregion

        #endregion

        #region 内部类
        public class 异常结束EventArgs : EventArgs
        {
            #region 属性
            public Exception exception = null;
            #endregion
            #region 构造器
            public 异常结束EventArgs() { }
            public 异常结束EventArgs(Exception e)
            {
                this.exception = e;
            }
            #endregion
        }
        public class 正常结束EventArgs : EventArgs
        {
        }
        public class 手动结束EventArgs : EventArgs
        {
        }

        /// <summary>
        /// 下面三个内部类是抽象类
        /// 要求“业务逻辑基类”的子类利用new关键字重新定义下面三个类
        /// </summary>
        public abstract class 测试输入项 { }
        public abstract class 测试数据EventArgs : EventArgs { }
        public abstract class 测试结果 { }
        
        #endregion
    }
}
