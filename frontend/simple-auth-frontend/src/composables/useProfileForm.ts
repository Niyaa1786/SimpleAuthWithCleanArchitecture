import { ref, onMounted } from 'vue'
import { storeToRefs } from 'pinia'
import { useUserProfileStore } from '@/stores/useUserProfileStore'
import { useAuthStore } from '@/stores/useAuthStore'
import { z } from 'zod'
import type { UpsertProfileRequest, Gender } from '@/types/user'

const profileSchema = z.object({
  firstName: z.string().min(1, 'Họ không được để trống').max(50, 'Họ tối đa 50 ký tự'),
  lastName: z.string().min(1, 'Tên không được để trống').max(50, 'Tên tối đa 50 ký tự'),
  phoneNumber: z
    .string()
    .regex(/^\+?[0-9\s\-\(\)]+$/, 'Số điện thoại không hợp lệ')
    .optional()
    .or(z.literal('')),
  gender: z.number().int().min(0).max(3),
})

export function useProfileForm() {
  const profileStore = useUserProfileStore()
  const { user } = storeToRefs(useAuthStore())
  const toast = useToast()
  const isLoading = ref(false)

  const formData = ref<Omit<UpsertProfileRequest, 'userId'>>({
    firstName: '',
    lastName: '',
    phoneNumber: '',
    gender: 0 as Gender,
  })

  const loadProfile = async () => {
    try {
      await profileStore.fetchMyProfile()
      if (profileStore.profile) {
        formData.value = {
          firstName: profileStore.profile.firstName,
          lastName: profileStore.profile.lastName,
          phoneNumber: profileStore.profile.phoneNumber || '',
          gender: profileStore.profile.gender,
        }
      }
    } catch (error) {
      toast.add({ title: 'Lỗi', description: (error as Error).message, color: 'error' })
    }
  }

  const onSubmit = async () => {
    if (!user.value) return
    isLoading.value = true
    try {
      const payload: UpsertProfileRequest = {
        userId: user.value.id,
        ...formData.value,
      }
      await profileStore.updateProfile(payload)
      toast.add({
        title: 'Thành công',
        description: 'Cập nhật hồ sơ thành công!',
        color: 'success',
      })
    } catch (error) {
      toast.add({ title: 'Lỗi', description: (error as Error).message, color: 'error' })
    } finally {
      isLoading.value = false
    }
  }

  onMounted(() => loadProfile())

  return { formData, isLoading, onSubmit, profileSchema }
}
