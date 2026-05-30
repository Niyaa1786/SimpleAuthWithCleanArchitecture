import { ref } from 'vue'
import { useUserProfileStore } from '@/stores/useUserProfileStore'

export function useAvatarUpload() {
  const profileStore = useUserProfileStore()
  const toast = useToast()
  const isUploading = ref(false)
  const isDeleting = ref(false)

  const uploadAvatar = async (file: File) => {
    isUploading.value = true
    try {
      const updatedProfile = await profileStore.uploadAvatar(file)
      toast.add({
        title: 'Thành công',
        description: 'Cập nhật ảnh đại diện thành công!',
        color: 'success',
      })
      return updatedProfile
    } catch (error) {
      toast.add({ title: 'Lỗi', description: (error as Error).message, color: 'error' })
      throw error
    } finally {
      isUploading.value = false
    }
  }

  const deleteAvatar = async () => {
    isDeleting.value = true
    try {
      await profileStore.deleteAvatar()
      toast.add({ title: 'Thành công', description: 'Đã xóa ảnh đại diện.', color: 'success' })
    } catch (error) {
      toast.add({ title: 'Lỗi', description: (error as Error).message, color: 'error' })
      throw error
    } finally {
      isDeleting.value = false
    }
  }

  return { isUploading, isDeleting, uploadAvatar, deleteAvatar }
}
