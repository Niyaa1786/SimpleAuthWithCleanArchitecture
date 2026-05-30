import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/useAuthStore'
import { useUserProfileStore } from '@/stores/useUserProfileStore'
import { authService } from '@/services/authService'

export function useLogout() {
  const router = useRouter()
  const authStore = useAuthStore()
  const profileStore = useUserProfileStore()

  const logout = async () => {
    try {
      if (authStore.user) {
        await authService.logout(authStore.user.id)
      }
    } catch (err) {
      // ignore logout error on client
    } finally {
      authStore.clearAuth()
      profileStore.clearProfile()
      router.push('/login')
    }
  }

  return { logout }
}
