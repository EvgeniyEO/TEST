using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using SharpDX.DirectInput;
using SharpDX;
using System.Windows.Forms;


namespace TestMAP
{
    static class Errors
    {
        public const long SUCCESS = 0;
        public const long ERROR = -1;

    }

    class JoystickDescriptor
    {
        public Guid DescriptorGuid;
        public string DescriptorName;

        public JoystickDescriptor(Guid InstanceGuid, string InstanceName)
        {
            DescriptorGuid = InstanceGuid;
            DescriptorName = InstanceName;
        }
    }
    class JoystickData
    {
        public Guid DescriptorGuid;
        public string DescriptorName;

        public JoystickData(Guid InstanceGuid, string InstanceName)
        {
            DescriptorGuid = InstanceGuid;
            DescriptorName = InstanceName;
        }
    }

    class DirectInputController : IDisposable
    {
        Packet_CA packet_CA = new Packet_CA();
        bool skip_data = false;
        private DirectInput directInput;
        private Thread pollingThread = null;
        private Joystick joystick = null;
        private bool Connect = false;
        bool _QuitPolling = false;
        public bool IsConnect() { return Connect; }

        public DirectInputController()
        {
            directInput = new DirectInput();
        }

        public void Dispose()
        {
            if (directInput != null) directInput.Dispose();
            if (joystick != null) joystick.Dispose();
        }

        public List<JoystickDescriptor> DetectDevices()
        {
            List<JoystickDescriptor> joystickDescriptors = new List<JoystickDescriptor>();

            // check for gamepads
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                joystickDescriptors.Add(new JoystickDescriptor(deviceInstance.InstanceGuid, deviceInstance.InstanceName));
            }

            // check for joysticks
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                joystickDescriptors.Add(new JoystickDescriptor(deviceInstance.InstanceGuid, deviceInstance.InstanceName));
            }
            
            return joystickDescriptors;
        }

        public long StartCapture(Guid joystickGuid)
        {
            long result = Errors.ERROR;

            if ( !directInput.IsDeviceAttached(joystickGuid) )
            {
                MessageBox.Show("Джойстик не найден, обновите список!", "Джойстик", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                joystick = new Joystick(directInput, joystickGuid);
                if (joystick != null)
                {
                    joystick.Properties.BufferSize = 128;
                    joystick.Acquire();

                    pollingThread = new Thread(new ThreadStart(PollJoystick));
                    if (pollingThread != null)
                    {
                        pollingThread.Start();
                        Connect = true;
                        result = Errors.SUCCESS;
                    }
                }
            }

            return result;
        }

        public void StopCapture()
        {
            if (pollingThread != null)
            {
                _QuitPolling = true;
                if (!pollingThread.Join(TimeSpan.FromMilliseconds(500)))
                {
                    pollingThread.Abort();
                }
                pollingThread = null;
            }

            if (joystick != null)
            {
                joystick.Unacquire();
                joystick.Dispose();
                joystick = null;
            }
            _QuitPolling = false;
            Connect = false;
        }

        public void PollJoystick()
        {
            while (!_QuitPolling)
            {
                if ( !directInput.IsDeviceAttached(joystick.Information.InstanceGuid) )
                {
                    MessageBox.Show("Потеряна связь с джойстиком", "Джойстик", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                joystick.Poll();
                JoystickUpdate[] datas = joystick.GetBufferedData();
                JoystickButtonPressedEventArgs args = new JoystickButtonPressedEventArgs();
                JoystickPacket_CAEventArgs argc_CA = new JoystickPacket_CAEventArgs();
                foreach (JoystickUpdate state in datas)
                {
                    skip_data = false;
                    switch (state.Offset)
                    {
                        case JoystickOffset.RotationX:
                            packet_CA.axis_X = (byte)(state.Value / 256);
                            args.Value = state.Value;
                            OnJoystickRotationXchange(args);
                            break;
                        case JoystickOffset.RotationY:
                            packet_CA.axis_Y = (byte)(state.Value / 256);
                            args.Value = state.Value;
                            OnJoystickRotationYchange(args);
                            break;
                        case JoystickOffset.X:
                            args.Value = state.Value;
                            OnJoystickXchange(args);
                            break;
                        case JoystickOffset.Y:
                            packet_CA.axis_Z = (byte)(state.Value / 256);
                            args.Value = state.Value;
                            OnJoystickYchange(args);
                            break;
                        case JoystickOffset.Z:
                            args.Value = state.Value;
                            OnJoystickZchange(args);
                            break;
                        case JoystickOffset.Buttons0:
                            packet_CA.button1 = (state.Value > 0) ? (byte)1 : (byte)0;
                            break;
                        default:
                            skip_data = true;
                            break;
                    }
                }
                if (datas.Length > 0 && !skip_data)
                {
                    argc_CA.packet_CA = packet_CA;
                    OnPacket_CA_change(argc_CA);
                }
                

                Thread.Sleep(10);
            }
        }

        protected virtual void OnJoystickXchange(JoystickButtonPressedEventArgs e)
        {
            EventHandler<JoystickButtonPressedEventArgs> handler = JoystickXchange;
            //EventHandler<JoystickButtonPressedEventArgs> handler = Volatile.Read(ref JoystickXchange);
            if (handler != null)
            {
                handler(this, e);
            }

        }

        public event EventHandler<JoystickButtonPressedEventArgs> JoystickXchange;

        protected virtual void OnJoystickYchange(JoystickButtonPressedEventArgs e)
        {
            EventHandler<JoystickButtonPressedEventArgs> handler = JoystickYchange;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<JoystickButtonPressedEventArgs> JoystickYchange;

        protected virtual void OnJoystickZchange(JoystickButtonPressedEventArgs e)
        {
            EventHandler<JoystickButtonPressedEventArgs> handler = JoystickZchange;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<JoystickButtonPressedEventArgs> JoystickZchange;

        protected virtual void OnJoystickRotationXchange(JoystickButtonPressedEventArgs e)
        {
            EventHandler<JoystickButtonPressedEventArgs> handler = JoystickRotationXchange;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<JoystickButtonPressedEventArgs> JoystickRotationXchange;

        protected virtual void OnJoystickRotationYchange(JoystickButtonPressedEventArgs e)
        {
            EventHandler<JoystickButtonPressedEventArgs> handler = JoystickRotationYchange;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<JoystickButtonPressedEventArgs> JoystickRotationYchange;

        protected virtual void OnPacket_CA_change(JoystickPacket_CAEventArgs e)
        {
            EventHandler<JoystickPacket_CAEventArgs> handler = Packet_CA_change;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<JoystickPacket_CAEventArgs> Packet_CA_change;
    }

    //public static class EventHandlerExtensions
    //{
    //    public static void SafeInvoke<T>(this EventHandler<T> evt, object sender, T e) where T : EventArgs
    //    {
    //        if (evt != null)
    //        {
    //            evt(sender, e);
    //        }
    //    }
    //}
    public class JoystickButtonPressedEventArgs : EventArgs
    {
        public int Value { get; set; }
//        public DateTime TimeStamp { get; set; }
    }
    public class JoystickPacket_CAEventArgs : EventArgs
    {
        public Packet_CA packet_CA { get; set; }
    }
    class DirectInputControllerOld
    {
        DirectInput directInput;
        Joystick joystick;
        public DirectInputControllerOld()
        {
            // Initialize DirectInput
            directInput = new DirectInput();
            directInput.GetDevices();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(SharpDX.DirectInput.DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            // Instantiate the joystick
            joystick = new Joystick(directInput, joystickGuid);

            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

            // Query all suported ForceFeedback effects
            var allEffects = joystick.GetEffects();
            foreach (var effectInfo in allEffects)
                Console.WriteLine("Effect available {0}", effectInfo.Name);

            // Set BufferSize in order to use buffered data.
            joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            joystick.Acquire();

            // Poll events from joystick
            //while (true)
            //{
            //    joystick.Poll();
            //    var datas = joystick.GetBufferedData();
            //    foreach (var state in datas)
            //        Console.WriteLine(state);
            //}
        }

        public void getData()
        {
            joystick.Poll();
            var datas = joystick.GetBufferedData();
            foreach (var state in datas)
            {
                if (state.Offset == JoystickOffset.X)
                {
                    Console.WriteLine("X = " + state.Value);
                }
                if (state.Offset == JoystickOffset.RotationY)
                {
                    Console.WriteLine("RotationY = " + state.Value);
                }
            }
            
        }
    }
}
