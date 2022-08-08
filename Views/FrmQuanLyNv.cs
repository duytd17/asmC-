using Asm_c_sharp_3.DomainClass;
using Asm_c_sharp_3.Repositories;
using Asm_c_sharp_3.Services;
using Asm_c_sharp_3.Utility;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Asm_c_sharp_3
{
    public partial class FrmQuanLyNV : Form
    {
        private NhanVienRepository _NvRepository;
        private NhanVienServicecs _qlNhanVienService;
        private Guid _idWhenClick;

        public FrmQuanLyNV()
        {
            InitializeComponent();
            _qlNhanVienService = new NhanVienServicecs();
            _NvRepository = new NhanVienRepository();
            txt_Ma.Enabled = false;
            rbtn_Nam.Checked = true;
            rbtn_KhongHoatDong.Checked = true;
            cmb_BaoCao.Enabled = false;
            LoadCuaHang();
            LoadChucVu();
            LoadBaoCao();
            LoadNhanVien(null);
            LoadLocCV();
            LoadLocCH();

        }

        private void LoadCuaHang()
        {
            foreach (var x in _NvRepository.LoadCuaHang())
            {
                cmb_CuaHang.Items.Add(x.Ma + '-' + x.Ten);
            }

            cmb_CuaHang.SelectedIndex = 0;


        }

        private void LoadChucVu()
        {
            foreach (var x in _NvRepository.LoadChucVu())
            {
                cmb_ChucVu.Items.Add(x.Ma + '-' + x.Ten);
            }


            cmb_ChucVu.SelectedIndex = 0;

        }

        private void LoadLocCV()
        {

            foreach (var x in _NvRepository.LoadChucVu())
            {
                cmb_LocCV.Items.Add(x.Ten);
            }
        }
        private void LoadLocCH()
        {

            foreach (var x in _NvRepository.LoadCuaHang())
            {
                cmb_LocMaCH.Items.Add(x.Ma);
            }
        }

        private void LoadBaoCao()
        {
            /* foreach (var x in _NvRepository.GetListNhanVienFromDB().Where(c => c.Ma.StartsWith("Q")))
             {
                 cmb_BaoCao.Items.Add(x.Ma + '-' +x.Ten);
             }
             cmb_BaoCao.SelectedIndex = 0;*/

            foreach (var x in _qlNhanVienService.GetAllNhanVien())
            {
                cmb_BaoCao.Items.Add(x.Ma + '-' + x.Ten);
            }
            /*if(cmb_BaoCao.SelectedIndex < 0)
            {
                cmb_BaoCao.SelectedIndex = 0;

            }*/
        }

        private void LoadNhanVien(string input)
        {
            int stt = 1;
            Type type = typeof(NhanVien);
            int SlThuocTinh = type.GetProperties().Length;
            dgrid_Data.ColumnCount = 13;
            dgrid_Data.Columns[0].Name = "STT";
            dgrid_Data.Columns[1].Name = "ID";
            dgrid_Data.Columns[1].Visible = false;
            dgrid_Data.Columns[2].Name = "Mã";
            dgrid_Data.Columns[3].Name = "Tên đầy đủ";
            dgrid_Data.Columns[4].Name = "Giới tính";
            dgrid_Data.Columns[5].Name = "Ngày sinh";
            dgrid_Data.Columns[6].Name = "Địa chỉ";
            dgrid_Data.Columns[7].Name = "SDT";
            dgrid_Data.Columns[8].Name = "Mật khẩu";
            dgrid_Data.Columns[9].Name = "Tên chức vụ";
            dgrid_Data.Columns[10].Name = "Tên cửa hàng";
            dgrid_Data.Columns[11].Name = "Tên người gửi";
            dgrid_Data.Columns[12].Name = "Trạng thái";
            dgrid_Data.Rows.Clear();
            foreach (var x in _qlNhanVienService.GetAll(input))
            {

                var macv = "";
                if (!string.IsNullOrEmpty(x.IdCv.ToString()))
                {
                    macv = _qlNhanVienService.GetAllChucVu().FirstOrDefault(c => c.Id == x.IdCv).Ten;
                }
                var mach = "";
                if (!string.IsNullOrEmpty(x.IdCv.ToString()))
                {
                    mach = _qlNhanVienService.GetAllCuaHang().FirstOrDefault(c => c.Id == x.IdCh).Ten;
                }
                var mabc = "";
                if (!string.IsNullOrEmpty(x.IdCv.ToString()))
                {
                    mabc = _qlNhanVienService.GetAllNhanVien().FirstOrDefault(c => c.Id == x.Id).Ten;
                }
                dgrid_Data.Rows.Add(
                    stt++,
                    x.Id,
                    x.Ma,
                    x.Ho + ' ' + x.TenDem + ' ' + x.Ten,
                    (x.GioiTinh == "1" ? "Nam" : "Nữ"),
                    x.NgaySinh,
                    x.DiaChi,
                    x.Sdt,
                    x.MatKhau, macv, mach, mabc,
                    (x.TrangThai == 1 ? "Hoạt động" : "Không hoạt động"))
                    ;
            }
        }


        private NhanVien GetDataFromGui()
        {
            return new NhanVien()
            {
                Id = Guid.Empty,
                Ma = Utilyti.GetMaTuSinh(txt_Ho.Text + txt_TenDem.Text + txt_Ten.Text) + _qlNhanVienService.GetAll().Count(),
                Ho = txt_Ho.Text,
                Ten = txt_Ten.Text,
                TenDem = txt_TenDem.Text,
                GioiTinh = (rbtn_Nam.Checked == true ? "1" : "0"),
                DiaChi = txt_DiaChi.Text,
                Sdt = txt_Sdt.Text,
                MatKhau = txt_Pass.Text,
                NgaySinh = DateTime.Parse(dateTimePicker1.Value.ToString("dd/MM/yyyy")),
                TrangThai = (rbtn_HoatDong.Checked == true ? 1 : 0),
                IdCv = _qlNhanVienService.GetAllChucVu()[cmb_ChucVu.SelectedIndex].Id,
                IdCh = _qlNhanVienService.GetAllCuaHang()[cmb_CuaHang.SelectedIndex].Id,
                // IdGuiBc = _qlNhanVienService.GetAllChucVu()[cmb_BaoCao.SelectedIndex].Id,

            };
            //Trả về 1 đối tượng chứa dữ liệu được lấy từ trên các control ở giao diện

        }
        bool KiemTraNhap()
        {
            string sdt = txt_Sdt.Text;
            long Ketqua;
            char[] MangSdt = sdt.ToCharArray();
            char[] mangPass = txt_Pass.Text.ToCharArray();

            errorProvider1.Clear();

            //dk họ tên rỗng
            if (txt_Ho.Text.Trim() == "" || txt_TenDem.Text.Trim() == "" || txt_Ten.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ họ tên", "Thông báo");


                txt_Ho.Focus();
                return false;
            }
            // dk địa chỉ rỗng
            if (txt_DiaChi.Text == "")
            {
                errorProvider1.SetError(txt_DiaChi, "Không được để trống");
                txt_DiaChi.Focus();
                return false;
            }
            //dk sdt rỗng
            if (txt_Sdt.Text == "")
            {
                errorProvider1.SetError(txt_Sdt, "Không được để trống");
                txt_Sdt.Focus();
                return false;
            }
            //dk pass rỗng
            if (txt_Pass.Text == "")
            {
                errorProvider1.SetError(txt_Pass, "Không được để trống");
                txt_Pass.Focus();
                return false;
            }
            //dk độ dài số điện thoại & chuẩn form số của VN
            if (!long.TryParse(sdt, out Ketqua) || MangSdt.Length != 10 || sdt.IndexOf("0") != 0)
            {
                errorProvider1.SetError(txt_Sdt, "Hãy nhập đúng định dạng số điện thoại VN");

                txt_Sdt.Focus();
                return false;
            }

            //dk pass > 3
            if (mangPass.Length <= 3)
            {
                errorProvider1.SetError(txt_Pass, "Mật khẩu phải lớn hơn 3 kí tự");
                txt_Pass.Focus();
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {



            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn thêm nhân viên này không ?", "Thông Báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {


                if (KiemTraNhap())
                {

                    var temp = GetDataFromGui();
                    temp.Id = _idWhenClick;
                    MessageBox.Show(_qlNhanVienService.AddNv(temp));

                    //clear toan bo control
                    foreach (var item in groupBox1.Controls)
                    {
                        TextBox item1 = item as TextBox;
                        if (item1 != null)
                        {
                            item1.Clear();
                        }
                    }
                    LoadNhanVien(null);

                }

            }
            if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn sửa nhân viên này không ?", "Thông Báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                var temp = GetDataFromGui();
                temp.Id = _idWhenClick;
                MessageBox.Show(_qlNhanVienService.UpdateNv(temp));
                LoadNhanVien(null);
            }
            if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này không ?", "Thông Báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var temp = GetDataFromGui();
                temp.Id = _idWhenClick;
                MessageBox.Show(_qlNhanVienService.DeleteNv(temp));
                LoadNhanVien(null);
            }
            if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa trắng toàn bộ thông tin đã nhập không?", "Thông Báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txt_Ma.Text = "";
                txt_Ho.Text = "";
                txt_Ho.Focus();
                txt_TenDem.Text = "";
                txt_Ten.Text = "";
                txt_DiaChi.Text = "";
                txt_Sdt.Text = "";
                txt_Pass.Text = "";
                cmb_BaoCao.Text = "";
                cmb_ChucVu.Text = "";
                cmb_CuaHang.Text = "";

            }
            if (dialogResult == DialogResult.No)
            {
                return;
            }


        }

        private void dgrid_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex == _qlNhanVienService.GetAll().Count) return;
            _idWhenClick = Guid.Parse(dgrid_Data.Rows[rowindex].Cells[1].Value.ToString());
            var nv = _qlNhanVienService.GetAll().FirstOrDefault(c => c.Id == _idWhenClick);
            txt_Ma.Text = nv.Ma;
            txt_Ho.Text = nv.Ho;
            txt_TenDem.Text = nv.TenDem;
            txt_Ten.Text = nv.Ten;

            txt_DiaChi.Text = nv.DiaChi;
            txt_Sdt.Text = nv.Sdt;
            txt_Pass.Text = nv.MatKhau;
            dateTimePicker1.Text = Convert.ToString(nv.NgaySinh);
            cmb_ChucVu.SelectedIndex = _qlNhanVienService.GetAllChucVu().FindIndex(c => c.Id == nv.IdCv);
            cmb_CuaHang.SelectedIndex = _qlNhanVienService.GetAllCuaHang().FindIndex(c => c.Id == nv.IdCh);
            /* cmb_BaoCao.SelectedIndex = _qlNhanVienService.GetAllNhanVien().FindIndex(c => c.Id == nv.Id);*/
            cmb_BaoCao.Text = Convert.ToString(dgrid_Data.Rows[rowindex].Cells[9].Value) + '-' + Convert.ToString(dgrid_Data.Rows[rowindex].Cells[11].Value);
            // cmb_CuaHang.SelectedIndex = cmb_CuaHang.FindStringExact(_qlNhanVienService.GetAllCuaHang().FirstOrDefault(c => c.Id == nv.IdCh).Ma.ToString());
            //cmb_ChucVu.SelectedIndex = cmb_ChucVu.FindStringExact(_qlNhanVienService.GetAllChucVu().FirstOrDefault(c => c.Id == nv.IdCv).Ma.ToString());
            if (nv.TrangThai == 1)
            {
                rbtn_KhongHoatDong.Checked = true;
                return;
            }
            rbtn_HoatDong.Checked = false;

            if (nv.GioiTinh == "Nam")
            {
                rbtn_Nam.Checked = true;
                return;
            }
            rbtn_Nu.Checked = false;

            return;
        }
        //hiển thị pass
        private void cbx_HienThiPass_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_HienThiPass.Checked)
            {
                txt_Pass.PasswordChar = '\0';
            }
            else
            {
                txt_Pass.PasswordChar = '*';
            }
        }

        //check nhập số điện thoại là số 
        private void txt_Sdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsWhiteSpace(e.KeyChar) && !char.IsPunctuation(e.KeyChar) && !char.IsSymbol(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Số điện thoại phải là số", " thông báo ");
                e.Handled = true;
                txt_Sdt.Clear();
                txt_Sdt.Focus();
            }
            return;
        }

        //chek nhập họ là chữ
        private void txt_Ho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsPunctuation(e.KeyChar) && !char.IsSymbol(e.KeyChar) && !char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Họ tên phải hợp lệ, không được chứa ký tự đặc biệt", " thông báo ");
                e.Handled = true;
                txt_Ho.Clear();
                txt_Ho.Focus();
            }
            return;
        }
        //chek nhập tên đệm là chữ
        private void txt_TenDem_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsPunctuation(e.KeyChar) && !char.IsSymbol(e.KeyChar) && !char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Họ tên phải hợp lệ, không được chứa ký tự đặc biệt", " thông báo ");

                e.Handled = true;
                txt_TenDem.Clear();
                txt_TenDem.Focus();
            }
            return;
        }
        //check tên là chữ
        private void txt_Ten_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsPunctuation(e.KeyChar) && !char.IsSymbol(e.KeyChar) && !char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Họ tên phải hợp lệ, không được chứa ký tự đặc biệt", " thông báo ");

                e.Handled = true;
                txt_Ten.Clear();
                txt_Ten.Focus();
            }
            return;
        }
        //cho họ auto viết hoa chữ cái đầu
        private void txt_Ho_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Ho.Text)) return;
            txt_Ho.Text = Utilyti.VietHoaChuCaiDau(txt_Ho.Text);
        }
        //cho tên đệm auto viết hoa chữ cái đầu
        private void txt_TenDem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_TenDem.Text)) return;
            txt_TenDem.Text = Utilyti.VietHoaChuCaiDau(txt_TenDem.Text);
        }
        //cho tên auto viết hoa chữ cái đầu
        private void txt_Ten_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Ten.Text)) return;
            txt_Ten.Text = Utilyti.VietHoaChuCaiDau(txt_Ten.Text);
        }
        //cho địa chỉ auto viết hoa chữ cái đầu
        private void txt_DiaChi_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_DiaChi.Text)) return;
            txt_DiaChi.Text = Utilyti.VietHoaChuCaiDau(txt_DiaChi.Text);
        }

        private void txt_TimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadNhanVien(txt_TimKiem.Text);
        }

        private void txt_TimKiem_MouseClick(object sender, MouseEventArgs e)
        {
            txt_TimKiem.Text = "";
        }

        private void txt_TimKiem_Leave(object sender, EventArgs e)
        {
            txt_TimKiem.Text = "Tìm kiếm...";
            LoadNhanVien(null);
        }

        /* public void LocTheoChucVu()
         {
             int stt = 1;
             dgrid_Data.ColumnCount = 13;
             dgrid_Data.Columns[0].Name = "STT";
             dgrid_Data.Columns[1].Name = "ID";
             dgrid_Data.Columns[1].Visible = false;
             dgrid_Data.Columns[2].Name = "Mã";
             dgrid_Data.Columns[3].Name = "Tên đầy đủ";
             dgrid_Data.Columns[4].Name = "Giới tính";
             dgrid_Data.Columns[5].Name = "Ngày sinh";
             dgrid_Data.Columns[6].Name = "Địa chỉ";
             dgrid_Data.Columns[7].Name = "SDT";
             dgrid_Data.Columns[8].Name = "Mật khẩu";
             dgrid_Data.Columns[9].Name = "Tên chức vụ";
             dgrid_Data.Columns[10].Name = "Tên cửa hàng";
             dgrid_Data.Columns[11].Name = "Tên người gửi";
             dgrid_Data.Columns[12].Name = "Trạng thái";
             dgrid_Data.Rows.Clear();
             foreach (var x in _qlNhanVienService.GetAllNhanVien().Where(c => c.Ma.StartsWith("Q")))
             {
                 var macv = _qlNhanVienService.GetAllChucVu().Find(c => c.Id == x.IdCv);
                 var mach = _qlNhanVienService.GetAllCuaHang().Find(c => c.Id == x.IdCh);
                 var mabc = _qlNhanVienService.GetAllNhanVien().Find(c => c.Id == x.Id);


                 dgrid_Data.Rows.Add(stt++, x.Id, x.Ma, x.Ho + ' ' + x.TenDem + ' ' + x.Ten, (x.GioiTinh == "1" ? "Nam" : "Nữ"), x.NgaySinh, x.DiaChi, x.Sdt,
                     x.MatKhau, macv.Ten, mach.Ten, mabc.Ten, (x.TrangThai == 1 ? "Hoạt động" : "Không hoạt động"));
             }
             return;
         }*/
        private void cmb_LocCV_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNhanVien(cmb_LocCV.SelectedText);
        }
    }
}
