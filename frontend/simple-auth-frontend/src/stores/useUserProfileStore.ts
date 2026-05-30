// src/stores/userProfileStore.ts
import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { UserProfileResponse, UpsertProfileRequest } from '@/types/user'
import { userProfileService } from '@/services/userProfileService'

export const useUserProfileStore = defineStore('userProfile', () => {
  const profile = ref<UserProfileResponse | null>(null)
  const allProfiles = ref<UserProfileResponse[]>([])

  async function fetchMyProfile() {
    const data = await userProfileService.getById()
    profile.value = data
    return data
  }

  async function fetchAllProfiles() {
    const data = await userProfileService.getAll()
    allProfiles.value = data
    return data
  }

  async function updateProfile(request: UpsertProfileRequest) {
    const updated = await userProfileService.upsertProfile(request)
    profile.value = updated
    return updated
  }

  async function uploadAvatar(file: File) {
    const updated = await userProfileService.uploadAvatar(file)
    profile.value = updated
    return updated
  }

  async function deleteAvatar() {
    await userProfileService.deleteAvatar()
    if (profile.value) {
      profile.value.avatarUrl = undefined
    }
  }

  function clearProfile() {
    profile.value = null
    allProfiles.value = []
  }

  return {
    profile,
    allProfiles,
    fetchMyProfile,
    fetchAllProfiles,
    updateProfile,
    uploadAvatar,
    deleteAvatar,
    clearProfile,
  }
})
