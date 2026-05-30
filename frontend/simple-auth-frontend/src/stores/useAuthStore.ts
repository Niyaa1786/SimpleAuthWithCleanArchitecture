import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { UserRole } from '@/types/user'
import type { AuthResponse, UserDto } from '@/types/auth'

export const useAuthStore = defineStore('auth', () => {
  const accessToken = ref<string | null>(null)
  const refreshToken = ref<string | null>(null)
  const user = ref<UserDto | null>(null)
  const accessTokenExpiration = ref<string | null>(null)
  const refreshTokenExpiration = ref<string | null>(null)

  const isAuthenticated = computed(() => !!accessToken.value && !!user.value)
  const isAdmin = computed(() => user.value?.role === UserRole.Admin)

  function setAuthData(data: AuthResponse) {
    accessToken.value = data.accessToken
    refreshToken.value = data.refreshToken
    user.value = data.user
    accessTokenExpiration.value = data.accessTokenExpiration
    refreshTokenExpiration.value = data.refreshTokenExpiration

    localStorage.setItem(
      'auth',
      JSON.stringify({
        accessToken: data.accessToken,
        refreshToken: data.refreshToken,
        user: data.user,
        accessTokenExpiration: data.accessTokenExpiration,
        refreshTokenExpiration: data.refreshTokenExpiration,
      }),
    )
  }

  function clearAuth() {
    accessToken.value = null
    refreshToken.value = null
    user.value = null
    accessTokenExpiration.value = null
    refreshTokenExpiration.value = null
    localStorage.removeItem('auth')
  }

  function loadFromStorage() {
    const stored = localStorage.getItem('auth')
    if (stored) {
      try {
        const data = JSON.parse(stored)
        accessToken.value = data.accessToken
        refreshToken.value = data.refreshToken
        user.value = data.user
        accessTokenExpiration.value = data.accessTokenExpiration
        refreshTokenExpiration.value = data.refreshTokenExpiration
      } catch (e) {
        clearAuth()
      }
    }
  }

  return {
    accessToken,
    refreshToken,
    user,
    accessTokenExpiration,
    refreshTokenExpiration,
    isAuthenticated,
    isAdmin,
    setAuthData,
    clearAuth,
    loadFromStorage,
  }
})
