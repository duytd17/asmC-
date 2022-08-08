using Asm_c_sharp_3.DomainClass;
using Asm_c_sharp_3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asm_c_sharp_3.Services
{
    public class CuaHangService
    {
        private List<CuaHang> _lstCuaHangs;
        private CuaHangRepository _chRepository;
        private NhanVienRepository _nvRepository;
    
        private CuaHangService()
        {
            _lstCuaHangs = new List<CuaHang>();
            _chRepository = new CuaHangRepository();
            _nvRepository = new NhanVienRepository();
            GetDataFromDB();
            NhanVien nv = new NhanVien() { Id = Guid.Empty};
        }

        public void GetDataFromDB()
        {
            _lstCuaHangs = _chRepository.GetAll();
        }

        public string Add(CuaHang ch)
        {
            if (_chRepository.AddCuaHang(ch))
            {
                GetDataFromDB();
                return "Thêm thành công!";
            }
            return "không thành công!";
        }

        public string Update(CuaHang ch)
        {
            int index = _lstCuaHangs.FindIndex(c => c.Id == ch.Id); 
            if(index == -1)
            {
                return "không tìm thấy !";
            }

            if (_chRepository.UpdateCuaHang(ch))
            {
                GetDataFromDB();
                return "sửa thành công!";
            }
            return "Không thành công!";
        }

        public string Delete(CuaHang ch)
        {
            int index = _lstCuaHangs.FindIndex(c => c.Id == ch.Id);
            if (index == -1)
            {
                return "không tìm thấy !";
            }

            if (_chRepository.DeleteCuaHang(ch))
            {
                GetDataFromDB();
                return "Xóa thành công!";
            }
            return "Không thành công!";
        }

        public List<CuaHang> GetAll()
        {
            return _lstCuaHangs;
        }

        public List<CuaHang>GetAll(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return GetAll();
            }
            return _chRepository.GetAll().Where(c=> c.Ten.ToLower().StartsWith(input.ToLower())).ToList();
        }
        /*public List<CuaHang> GetAllCuaHang()
       {
           return CuaHangRepository.GetAll().ToList();
       }*/
    }
}
