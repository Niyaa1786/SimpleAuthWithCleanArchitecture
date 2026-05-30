import { z } from 'zod'

// Schema đăng nhập – chỉ yêu cầu username hoặc email + password
export const loginSchema = z.object({
  username: z.string().min(1, 'Vui lòng nhập tên đăng nhập hoặc email'),
  // Trong thực tế username có thể chứa email, nhưng ta tạm coi username là bắt buộc
  password: z.string().min(1, 'Vui lòng nhập mật khẩu'),
})

// Schema đăng ký – các rule cơ bản
export const registerSchema = z.object({
  username: z.string().min(6, 'Tên đăng nhập phải có ít nhất 6 ký tự'),
  email: z.email('Email không hợp lệ'),
  password: z.string().min(8, 'Mật khẩu phải có ít nhất 8 ký tự'),
})

// Schema cập nhật hồ sơ
export const profileSchema = z.object({
  firstName: z.string().min(1, 'Tên không được để trống').max(50, 'Tên tối đa 50 ký tự'),
  lastName: z.string().min(1, 'Họ không được để trống').max(50, 'Họ tối đa 50 ký tự'),
  phoneNumber: z.string().optional().or(z.literal('')),
  gender: z.number().int().min(0).max(3), // dùng số, không cần regex
})
