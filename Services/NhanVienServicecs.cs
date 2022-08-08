using Asm_c_sharp_3.DomainClass;
using Asm_c_sharp_3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asm_c_sharp_3.Services
{
    public class NhanVienServicecs
    {
        private List<NhanVien> _lstNhanViens;
        private NhanVienRepository _nvRepository;
        private ChucVuRepository _cvRepository;
        private CuaHangRepository _chRepository;
        
        public NhanVienServicecs()
        {
            _lstNhanViens = new List<NhanVien>();
            _nvRepository = new NhanVienRepository(); 
            _cvRepository = new ChucVuRepository();
            _chRepository = new CuaHangRepository();
            GetDataFromDB();
        }

        private void GetDataFromDB()
        {
            _lstNhanViens = _nvRepository.GetAll();
        }

        public string AddNv(NhanVien nv)
        {
            if (_nvRepository.AddNhanVien(nv))
            {
                GetDataFromDB();
                return "Thêm thành công!";
            }
            return "Không thành công!";
        }

        public string UpdateNv(NhanVien nv)
        {
            int index = _lstNhanViens.FindIndex(c => c.Id == nv.Id);
            if(index == -1)
            {
                return "không tìm thấy!";
            }

            if (_nvRepository.UpdateNhanVien(nv))
            {
                GetDataFromDB();
                return "Sửa thành công !";
            }
            return "không thành công!";
        }

        public string DeleteNv(NhanVien nv)
        {
            int index = _lstNhanViens.FindIndex(c => c.Id == nv.Id);
            if (index == -1)
            {
                return "không tìm thấy!";
            }

            if (_nvRepository.DeleteNhanVien(nv))
            {
                GetDataFromDB();
                return "Xóa thành công !";
            }
            return "không thành công!";
        }

        public List<NhanVien> GetAll()
        {
            return _lstNhanViens;

        }


        public List<NhanVien>GetAll(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return GetAll();
            }
            return _nvRepository.GetAll().Where(c => c.Ten.ToLower().StartsWith(input.ToLower())||c.Ma.ToLower().StartsWith(input.ToLower())).ToList();  
        }

        public List<NhanVien> GetAllNhanVien()
        {
            return _nvRepository.GetAll().ToList();
        }

        public List<CuaHang> GetAllCuaHang()
        {
            return _chRepository.GetAll().ToList();
        }

        public List<ChucVu> GetAllChucVu()
        {
            return _cvRepository.GetAll().ToList();
        }
    }
}
