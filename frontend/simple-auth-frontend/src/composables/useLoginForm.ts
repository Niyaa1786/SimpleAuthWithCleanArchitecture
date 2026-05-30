import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/useAuthStore'
import { authService } from '@/services/authService'
import { z } from 'zod'
import type { LoginRequest } from '@/types/auth'

const loginSchema = z
  .object({
    username: z.string().min(1, 'Tên đăng nhập không được để trống').optional(),
    email: z.email('Email không hợp lệ').optional(),
    password: z.string().min(6, 'Mật khẩu tối thiểu 6 ký tự'),
  })
  .refine((data) => data.username || data.email, {
    message: 'Vui lòng nhập tên đăng nhập hoặc email',
    path: ['username'],
  })

export function useLoginForm() {
  const router = useRouter()
  const authStore = useAuthStore()
  const toast = useToast()
  const isLoading = ref(false)

  const onSubmit = async (data: LoginRequest) => {
    isLoading.value = true
    try {
      const response = await authService.login(data)
      authStore.setAuthData(response)
      toast.add({ title: 'Thành công', description: 'Đăng nhập thành công!', color: 'success' })
      router.push('/profile')
    } catch (err) {
      toast.add({ title: 'Lỗi', description: (err as Error).message, color: 'error' })
    } finally {
      isLoading.value = false
    }
  }

  return { isLoading, onSubmit, loginSchema }
}
