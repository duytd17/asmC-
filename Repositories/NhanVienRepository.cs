using Asm_c_sharp_3.Context;
using Asm_c_sharp_3.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asm_c_sharp_3.Repositories
{
    public class NhanVienRepository
    {
        private FpolyDBContext _dbConText;
        private List<NhanVien> _lstNhanVien;
        private List<ChucVu> _lstChucVus;
        private List<CuaHang> _lstCuaHangs;

        public NhanVienRepository()
        {
            _dbConText = new FpolyDBContext();
            _lstNhanVien = new List<NhanVien>();         
        }

     

        public bool AddNhanVien(NhanVien nv)
        {
            if (nv == null) return false;
            nv.Id = Guid.NewGuid();            
            _dbConText.NhanViens.Add(nv);
            _dbConText.SaveChanges();
            return true;
        }

        public bool UpdateNhanVien( NhanVien nv)
        {
            if(nv == null) return false;    
            var tempnv = _dbConText.NhanViens.FirstOrDefault(c => c.Id == nv.Id);
            tempnv.Ten = nv.Ten;
            tempnv.TenDem = nv.TenDem;
            tempnv.Ho = nv.Ho;
            tempnv.GioiTinh = nv.GioiTinh;
            tempnv.NgaySinh = nv.NgaySinh;
            tempnv.DiaChi = nv.DiaChi;
            tempnv.Sdt = nv.Sdt;
            tempnv.MatKhau = nv.MatKhau;
            tempnv.TrangThai = nv.TrangThai;
            tempnv.IdCh = nv.IdCh;
            tempnv.IdCv = nv.IdCv;
            _dbConText.Update(tempnv);
            _dbConText.SaveChanges();
            return true;
        }

        public bool DeleteNhanVien(NhanVien nv)
        {
            if(nv==null) return false;
            var tempnv = _dbConText.NhanViens.FirstOrDefault(c => c.Id == nv.Id);
            _dbConText.Remove(tempnv);
            _dbConText.SaveChanges();
            return true;
        }

        public List<NhanVien> GetAll()
        {
            return _dbConText.NhanViens.ToList();
        }

        public List<NhanVien> LoadNhanVien()
        {
            return _lstNhanVien = _dbConText.NhanViens.ToList();
        }

        public List<ChucVu> LoadChucVu()
        {
            return _lstChucVus = _dbConText.ChucVus.ToList();
        }

        public List<CuaHang> LoadCuaHang()
        {
            return _lstCuaHangs = _dbConText.CuaHangs.ToList();
        }
    }
}
