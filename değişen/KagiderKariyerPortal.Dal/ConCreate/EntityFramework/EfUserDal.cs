using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfUserDal:IEntityDal<User>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public User Get(User entity)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == entity.UserId);
        }

        public List<User> GetList()
        {
            return _context.Users.ToList();
        }

        public void Del(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            User updateUser=_context.Users.FirstOrDefault(u => u.UserId == entity.UserId);
            if (updateUser != null)
            {
                updateUser.UserName = entity.UserName;
                updateUser.MemberName= entity.MemberName;
                updateUser.MemberSurName = entity.MemberSurName;
                updateUser.MobilPhone = entity.MobilPhone;
                
           
                updateUser.Password = entity.Password;
                updateUser.Email = entity.Email;
                updateUser.ResimYol = entity.ResimYol;
            }
            _context.SaveChanges();
        }
    }
}
