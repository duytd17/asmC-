using Asm_c_sharp_3.DomainClass;
using Asm_c_sharp_3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asm_c_sharp_3.Services
{
    public class ChucVuService
    {
        private List<ChucVu> _lstChucVus;
        private ChucVuRepository _cvRepository;
        private NhanVienRepository _nhanVienRepository;

        public ChucVuService() 
        { 
            _lstChucVus = new List<ChucVu>();
            _cvRepository = new ChucVuRepository();
            _nhanVienRepository = new NhanVienRepository();
            NhanVien nv = new NhanVien() { Id = Guid.Empty };
            GetDataFromDB();
        }

        private void GetDataFromDB()
        {
            _lstChucVus = _cvRepository.GetAll();
        }

        public string Add(ChucVu cv)
        {
            if (_cvRepository.AddChucVu(cv))
            {
                GetDataFromDB();
                return "Thêm thành công!";
            }
            return "Không thành công";
        }


        public string Update(ChucVu cv)
        {
            int index = _lstChucVus.FindIndex(c => c.Id == cv.Id);
            if (index == -1)
            {
                return "Không tìm thấy";
            }

            if (_cvRepository.UpdateChucVu(cv))
            {
                GetDataFromDB();
                return "Sửa thành công";
            }
            return "Không thành công";
        }

        public string Delete(ChucVu cv)
        {
            int index = _lstChucVus.FindIndex(c => c.Id == cv.Id);
            if (index == -1)
            {
                return "Không tìm thấy";
            }

            if (_cvRepository.DeleteChucVu(cv))
            {
                GetDataFromDB();
                return "Xóa thành công";
            }
            return "Không thành công";
        }

        public List<ChucVu> GetAll()
        {
            return _lstChucVus;
        }

        public List<ChucVu> GetAll(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return GetAll();
            }
            return _cvRepository.GetAll().Where(c => c.Ten.ToLower().StartsWith(input.ToLower())).ToList();
        }
        /*public List<CuaHang> GetAllCuaHang()
        {
            return CuaHangRepository.GetAll().ToList();
        }*/
    }
}
