import axios from 'axios'
import type { ApiResponse } from '@/types/api'
import type { AuthResponse } from '@/types/auth'
import { useAuthStore } from '@/stores/useAuthStore'

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: { 'Content-Type': 'application/json' },
})

axiosInstance.interceptors.request.use((config) => {
  const authStore = useAuthStore()
  if (authStore.accessToken) {
    config.headers.Authorization = `Bearer ${authStore.accessToken}`
  }
  return config
})

axiosInstance.interceptors.response.use(
  (response) => {
    return response
  },
  async (error) => {
    const originalRequest = error.config
    const authStore = useAuthStore()

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true

      try {
        const { refreshToken } = authStore
        if (!refreshToken) throw new Error('No refresh token')

        const { data } = await axiosInstance.post<ApiResponse<AuthResponse>>(
          '/Auth/RefreshToken',
          refreshToken,
        )

        if (data.isSuccess && data.data) {
          authStore.setAuthData(data.data)

          originalRequest.headers.Authorization = `Bearer ${data.data.accessToken}`
          return axiosInstance(originalRequest)
        }
      } catch (refreshError) {
        authStore.clearAuth()
        return Promise.reject(refreshError)
      }
    }

    return Promise.reject(error)
  },
)

export default axiosInstance
