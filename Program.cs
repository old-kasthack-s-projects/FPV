﻿using System;
using System.IO;
using System.Windows.Forms;
    public class fpv : Form{
        string ls; int index = 0, error = 0; bool alt = false; string[] files=new string[]{};
        public static void Main(string[] args){ try{Application.EnableVisualStyles(); Application.Run(new fpv(args.Length>0? args[0]:""));}catch { }}
        public void draw(int a){
					try{if (error < files.Length){
                    this.BackgroundImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes((files[index = ((files.Length + (index + a)) % files.Length)]))));
                    this.Text = Path.GetFileName( files[index % files.Length]) + " -- kasthack's Fast Photo Viewer";
                    files = Directory.GetFiles(ls); }
					error = 0;}catch { error++; draw(a); } GC.Collect();}
        private void pctPicture_Click(object sender, MouseEventArgs e){
            bool f = this.WindowState == FormWindowState.Maximized;
            WindowState = f ? FormWindowState.Normal : FormWindowState.Maximized;
            FormBorderStyle = f ? FormBorderStyle.Sizable : FormBorderStyle.None;
            BackColor = f ? System.Drawing.Color.White : System.Drawing.Color.Black;}
        public void frmview_KeyUp(object sender, KeyEventArgs e){ if ((e.KeyCode == Keys.A)||(e.KeyCode==(Keys.RButton|Keys.ShiftKey))) alt = false;}
        private void frmview_KeyDown(object sender, KeyEventArgs e){
            switch (e.KeyCode){
                case Keys.Enter:  if (!alt) draw(++index); else pctPicture_Click(null, null); break;
                case Keys.Alt: case Keys.RButton|Keys.ShiftKey: case Keys.LButton|Keys.ShiftKey: alt = true; break;
                case Keys.Right: case Keys.D: case Keys.PageDown: case Keys.Down: case Keys.NumPad6: case Keys.NumPad2: case Keys.S: case Keys.Space: draw(1); break;
                case Keys.Left: case Keys.A: case Keys.Up: case Keys.W: case Keys.PageUp: case Keys.NumPad8: case Keys.NumPad4: draw(- 1); break;
                case Keys.Q: Application.Exit(); break;
                case Keys.F11: pctPicture_Click(null, null); break;
                case Keys.Delete: if (MessageBox.Show("Do U really want to delete"+files[index]+"?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes) try { File.Delete(files[index]); draw(1); }catch { }this.Activate(); break;
                case Keys.Escape: if (this.WindowState == FormWindowState.Maximized) pctPicture_Click(null, null); else Application.Exit(); break;
                case Keys.F1: case Keys.H: MessageBox.Show("FPV by kasthack v 0.5.1.\r\nKeys:\r\nD/S/↓/→/Num6/Num2/Space - next photo;\r\nA/W/↑/←/Num8/Num4 - previous photo;\r\nF11/Alt+Enter - fullscreen;\r\nEsc - exit fullscreen;\r\nEsc-Esc/Q - exit FPV;\r\nF1/H - show this message.", "FPV:Help", MessageBoxButtons.OK, MessageBoxIcon.Information); this.Activate(); break;}}
        protected override void Dispose(bool disposing){base.Dispose(disposing);}
        public fpv(string init){
            try
            {
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.MouseDoubleClick += new MouseEventHandler(pctPicture_Click);
				DoubleBuffered=true;
                ClientSize = new System.Drawing.Size(284, 262);
                KeyUp += new KeyEventHandler(frmview_KeyUp);
                KeyDown += new System.Windows.Forms.KeyEventHandler(frmview_KeyDown);
                ls = Path.GetDirectoryName(init);
                files = Directory.GetFiles(ls);
            }
            catch { frmview_KeyDown(null, new KeyEventArgs(Keys.F1)); }
            try { this.BackgroundImage = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(init))); this.Text = Path.GetFileName(init) + " -- kasthack's Fast Photo Viewer"; }
            catch { draw(1); }}}