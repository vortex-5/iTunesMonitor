using System;
using System.Windows.Forms; // for Key namespace
using System.Runtime.InteropServices;



namespace itunes_monitor
{
	/// <summary>
	/// Summary description for NativeWIN32.
	/// </summary>
	public class NativeWIN32
	{
		public NativeWIN32()
		{}
/* ------- using HOTKEYs in a C# application -------

 in form load :
	bool success = RegisterHotKey(Handle, 100,     KeyModifiers.Control | KeyModifiers.Shift, Keys.J);

 in form closing :
	UnregisterHotKey(Handle, 100);
 

 protected override void WndProc( ref Message m )
 {	
	const int WM_HOTKEY = 0x0312; 	
	
	switch(m.Msg)	
	{	
		case WM_HOTKEY:		
			MessageBox.Show("Hotkey pressed");		
			break;	
	} 	
	base.WndProc(ref m );
}

------- using HOTKEYs in a C# application ------- */

		[DllImport("user32.dll", SetLastError=true)]
		private static extern bool RegisterHotKey(	IntPtr hWnd, // handle to window    
			int id,            // hot key identifier    
			KeyModifiers fsModifiers,  // key-modifier options    
			Keys vk            // virtual-key code    
			); 
		
		[DllImport("user32.dll", SetLastError=true)]
		private static extern bool UnregisterHotKey(	IntPtr hWnd,  // handle to window    
			int id      // hot key identifier    
			);

		[Flags()]
			public enum KeyModifiers
		{  
			None = 0,
			Alt = 1,    
			Control = 2,    
			Shift = 4,    
			Windows = 8
		}

        public static bool UnregisterHotkeys(IntPtr Handle)
        {
            try
            {
                NativeWIN32.UnregisterHotKey(Handle, (int)Keys.MediaNextTrack);
                NativeWIN32.UnregisterHotKey(Handle, (int)Keys.MediaPlayPause);
                NativeWIN32.UnregisterHotKey(Handle, (int)Keys.MediaPreviousTrack);
                NativeWIN32.UnregisterHotKey(Handle, (int)Keys.MediaStop);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool RegisterHotKeys(IntPtr Handle)
        {
            try
            {
                SetHotKey(Handle, Keys.MediaNextTrack, false,false,false,(int)Keys.MediaNextTrack);
                SetHotKey(Handle, Keys.MediaPlayPause, false, false, false, (int)Keys.MediaPlayPause);
                SetHotKey(Handle, Keys.MediaPreviousTrack, false, false, false, (int)Keys.MediaPreviousTrack);
                SetHotKey(Handle, Keys.MediaStop, false, false, false, (int)Keys.MediaStop);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void SetHotKey(IntPtr Handle, Keys c, bool bCtrl, bool bShift, bool bAlt, int id)
        {
            Keys m_hotkey = c;
            bool m_ctrlhotkey = bCtrl;
            bool m_shifthotkey = bShift;
            bool m_althotkey = bAlt;

            // update hotkey
            NativeWIN32.KeyModifiers modifiers = NativeWIN32.KeyModifiers.None;
            if (m_ctrlhotkey)
                modifiers |= NativeWIN32.KeyModifiers.Control;
            if (m_shifthotkey)
                modifiers |= NativeWIN32.KeyModifiers.Shift;
            if (m_althotkey)
                modifiers |= NativeWIN32.KeyModifiers.Alt;

            NativeWIN32.RegisterHotKey(Handle, id, modifiers, m_hotkey);
        }

	}
}
