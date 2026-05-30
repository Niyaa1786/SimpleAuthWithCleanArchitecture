import axiosInstance from '@/api/axiosInstance'
import type { ApiResponse } from '@/types/api'
import type { LoginRequest, RegisterRequest, RefreshTokenRequest, AuthResponse } from '@/types/auth'

export const authService = {
  async register(data: RegisterRequest): Promise<AuthResponse> {
    const res = await axiosInstance.post<ApiResponse<AuthResponse>>('/Auth/Register', data)
    if (!res.data.isSuccess) throw new Error(res.data.message)
    return res.data.data!
  },

  async login(data: LoginRequest): Promise<AuthResponse> {
    const res = await axiosInstance.post<ApiResponse<AuthResponse>>('/Auth/Login', data)
    if (!res.data.isSuccess) throw new Error(res.data.message)
    return res.data.data!
  },

  async logout(userId: string): Promise<void> {
    const res = await axiosInstance.post<ApiResponse<object>>(`/Auth/Logout?id=${userId}`)
    if (!res.data.isSuccess) throw new Error(res.data.message)
  },

  async refreshToken(data: RefreshTokenRequest): Promise<AuthResponse> {
    const res = await axiosInstance.post<ApiResponse<AuthResponse>>('/Auth/RefreshToken', data)
    if (!res.data.isSuccess) throw new Error(res.data.message)
    return res.data.data!
  },
}
