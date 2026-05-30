<script setup lang="ts">
import { useProfileForm } from '@/composables/useProfileForm'
import { useAvatarUpload } from '@/composables/useAvatarUpload'
import { useLogout } from '@/composables/useLogout'
import { useUserProfileStore } from '@/stores/useUserProfileStore'
import { storeToRefs } from 'pinia'
import { Gender } from '@/types/user'
import { ref } from 'vue'

const { formData, isLoading, onSubmit, profileSchema } = useProfileForm()
const { isUploading, isDeleting, uploadAvatar, deleteAvatar } = useAvatarUpload()
const { logout } = useLogout()
const profileStore = useUserProfileStore()
const { profile } = storeToRefs(profileStore)

const fileInputRef = ref<HTMLInputElement>()

// Dùng đúng giá trị number cho Gender
const genderOptions = [
  { label: 'Không xác định', value: Gender.Unknown },
  { label: 'Nam', value: Gender.Male },
  { label: 'Nữ', value: Gender.Female },
  { label: 'Khác', value: Gender.Other },
]

const handleFormSubmit = () => {
  onSubmit()
}

const handleFileChange = async (event: Event) => {
  const target = event.target as HTMLInputElement
  const file = target.files?.[0]
  if (!file) return
  await uploadAvatar(file)
  if (fileInputRef.value) fileInputRef.value.value = ''
}

const handleDeleteAvatar = async () => {
  if (confirm('Bạn có chắc muốn xóa ảnh đại diện?')) {
    await deleteAvatar()
  }
}
</script>

<template>
  <div class="min-h-screen bg-gray-50 py-8">
    <UContainer>
      <UCard>
        <template #header>
          <div class="flex justify-between items-center">
            <h1 class="text-2xl font-bold">Thông tin cá nhân</h1>
            <UButton color="error" variant="outline" @click="logout">Đăng xuất</UButton>
          </div>
        </template>

        <div class="flex flex-col md:flex-row gap-8">
          <div class="flex flex-col items-center space-y-4">
            <UAvatar
              :src="profile?.avatarUrl"
              :alt="`${formData.firstName} ${formData.lastName}`"
              size="2xl"
            />
            <div class="flex gap-2">
              <UButton size="sm" @click="fileInputRef?.click()" :loading="isUploading">
                Tải ảnh lên
              </UButton>
              <UButton
                v-if="profile?.avatarUrl"
                size="sm"
                color="error"
                variant="outline"
                :loading="isDeleting"
                @click="handleDeleteAvatar"
              >
                Xóa ảnh
              </UButton>
            </div>
            <input
              ref="fileInputRef"
              type="file"
              accept="image/*"
              class="hidden"
              @change="handleFileChange"
            />
          </div>

          <div class="flex-1">
            <UForm
              :state="formData"
              :schema="profileSchema"
              class="space-y-4"
              @submit="handleFormSubmit"
            >
              <UFormField label="Họ" name="firstName">
                <UInput v-model="formData.firstName" placeholder="Họ" />
              </UFormField>
              <UFormField label="Tên" name="lastName">
                <UInput v-model="formData.lastName" placeholder="Tên" />
              </UFormField>
              <UFormField label="Số điện thoại" name="phoneNumber">
                <UInput v-model="formData.phoneNumber" placeholder="Số điện thoại" />
              </UFormField>
              <UFormField label="Giới tính" name="gender">
                <USelect v-model="formData.gender" :items="genderOptions" />
              </UFormField>
              <UButton type="submit" :loading="isLoading" block>Cập nhật hồ sơ</UButton>
            </UForm>
          </div>
        </div>
      </UCard>
    </UContainer>
  </div>
</template>
