import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/useAuthStore'
import { authService } from '@/services/authService'
import { z } from 'zod'
import type { RegisterRequest } from '@/types/auth'

const registerSchema = z.object({
  username: z.string().min(6, 'Tên đăng nhập ít nhất 6 ký tự'),
  email: z.email('Email không hợp lệ'),
  password: z
    .string()
    .min(8, 'Mật khẩu ít nhất 8 ký tự')
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])/,
      'Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt',
    ),
})

export function useRegisterForm() {
  const router = useRouter()
  const authStore = useAuthStore()
  const toast = useToast()
  const isLoading = ref(false)

  const onSubmit = async (data: RegisterRequest) => {
    isLoading.value = true
    try {
      const response = await authService.register(data)
      authStore.setAuthData(response)
      toast.add({ title: 'Thành công', description: 'Đăng ký thành công!', color: 'success' })
      router.push('/profile')
    } catch (err) {
      toast.add({ title: 'Lỗi', description: (err as Error).message, color: 'error' })
    } finally {
      isLoading.value = false
    }
  }

  return { isLoading, onSubmit, registerSchema }
}
