using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Check02001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hienthi();
        }
        private void Hienthi()
        {
            string link = "http://localhost/Check01/api/nv/ds";
            HttpWebRequest req = HttpWebRequest.CreateHttp(link);
            WebResponse res = req.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(NhanVien[]));
            object data = js.ReadObject(res.GetResponseStream());
            NhanVien[] ds = (NhanVien[])data;
            dgrDanhSach.DataSource = ds;

        }
        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            Hienthi();
        }
        private void ClearBox()
        {
            txtMa.Clear();
            txtTen.Clear();
            txtHs.Clear();
            txtNhap.Clear();
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            string str = string.Format("?MaNV={0}", txtNhap.Text);
            string link = "http://localhost/Check01/api/nv/timnv" + str;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(NhanVien));
            object data = js.ReadObject(response.GetResponseStream());
            NhanVien nv = (NhanVien)data;
            if (nv != null)
            {
                txtMa.Text = nv.MaNV;
                txtTen.Text = nv.TenNV;
                txtHs.Text = nv.HSLuong;
            }
            else
            {
                MessageBox.Show("Khong co nhan vien co ma" + txtNhap.Text, "Thoong bao");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string link = "http://localhost/Check01/api/nv/them";
            var client = new WebClient();
            var nv = new NameValueCollection();
            nv["MaNV"] = txtMa.Text;
            nv["TenNV"] = txtTen.Text;
            nv["HSLuong"] = txtHs.Text;
            var res = client.UploadValues(link, nv);
            string msg = Encoding.UTF8.GetString(res);
            MessageBox.Show("Da them" + msg);
            Hienthi();
            ClearBox();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string link = "http://localhost/Check01/api/nv/sua";
            var client = new WebClient();
            var nv = new NameValueCollection();
            nv["MaNV"] = txtMa.Text;
            nv["TenNV"] = txtTen.Text;
            nv["HSLuong"] = txtHs.Text;
            var res = client.UploadValues(link, "PUT", nv);
            string msg = Encoding.UTF8.GetString(res);
            MessageBox.Show("Da sua" + msg);
            Hienthi();
            ClearBox();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string str = string.Format("?ma={0}", txtNhap.Text);
            string link = "http://localhost/Check01/api/nv/xoa"+str;
            WebRequest req = WebRequest.CreateHttp(link);
            req.Method = "Delete";
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            if (res.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Da xoa" + txtNhap.Text);
                Hienthi();
            }
            else
            {
                MessageBox.Show("Loi khi xoa" + txtNhap.Text);
            }
        }
    }
}
