using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using SharpDX.DirectInput;
using SharpDX;

namespace TestMAP
{
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
    class DirectInputController : IDisposable
    {
        private DirectInput directInput;
        private Thread pollingThread;
        private Joystick joystick;
        private int startButtonOffset = -1;
        private int lapButtonOffset = -1;
        bool _QuitPolling = false;

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


        public void StartCapture(Guid joystickGuid, int startButtonOffset, int lapButtonOffset)
        {
            this.startButtonOffset = startButtonOffset;
            this.lapButtonOffset = lapButtonOffset;
            StartCapture(joystickGuid);
        }

        public void StartCapture(Guid joystickGuid)
        {
            joystick = new Joystick(directInput, joystickGuid);

            joystick.Properties.BufferSize = 128;

            joystick.Acquire();

            pollingThread = new Thread(new ThreadStart(PollJoystick));
            pollingThread.Start();

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
            }

            if (joystick != null)
            {
                joystick.Dispose();
            }
        }

        public void PollJoystick()
        {
            while (!_QuitPolling)
            {
                joystick.Poll();
                JoystickUpdate[] datas = joystick.GetBufferedData();
                JoystickButtonPressedEventArgs args = new JoystickButtonPressedEventArgs();
                foreach (JoystickUpdate state in datas)
                {
                    switch (state.Offset)
                    {
                        case JoystickOffset.RotationX:
                            args.Value = state.Value;
                            OnJoystickRotationXchange(args);
                            break;
                        case JoystickOffset.RotationY:
                            args.Value = state.Value;
                            OnJoystickRotationYchange(args);
                            break;
                        case JoystickOffset.X:
                            args.Value = state.Value;
                            OnJoystickXchange(args);
                            break;
                        case JoystickOffset.Y:
                            args.Value = state.Value;
                            OnJoystickYchange(args);
                            break;
                        default:
                            break;
                    }
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
