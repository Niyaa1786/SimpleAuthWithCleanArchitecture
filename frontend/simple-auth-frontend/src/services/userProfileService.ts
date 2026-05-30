import axiosInstance from '@/api/axiosInstance'
import type { ApiResponse } from '@/types/api'
import type { UserProfileResponse, UpsertProfileRequest } from '@/types/user'

export const userProfileService = {
  async getAll(): Promise<UserProfileResponse[]> {
    const res = await axiosInstance.get<ApiResponse<UserProfileResponse[]>>('/UserProfiles/GetAll')
    if (!res.data.isSuccess) throw new Error(res.data.message)
    return res.data.data!
  },

  async getById(): Promise<UserProfileResponse> {
    const res = await axiosInstance.get<ApiResponse<UserProfileResponse>>('/UserProfiles/GetById')
    if (!res.data.isSuccess) throw new Error(res.data.message)
    return res.data.data!
  },

  async upsertProfile(data: UpsertProfileRequest): Promise<UserProfileResponse> {
    const res = await axiosInstance.post<ApiResponse<UserProfileResponse>>(
      '/UserProfiles/UpsertProfile',
      data,
    )
    if (!res.data.isSuccess) throw new Error(res.data.message)
    return res.data.data!
  },

  async uploadAvatar(file: File): Promise<UserProfileResponse> {
    const formData = new FormData()
    formData.append('file', file)
    const res = await axiosInstance.post<ApiResponse<UserProfileResponse>>(
      '/UserProfiles/UploadAvatar',
      formData,
      {
        headers: { 'Content-Type': 'multipart/form-data' },
      },
    )
    if (!res.data.isSuccess) throw new Error(res.data.message)
    return res.data.data!
  },

  async deleteAvatar(): Promise<void> {
    const res = await axiosInstance.delete<ApiResponse<object>>('/UserProfiles/DeleteProfile')
    if (!res.data.isSuccess) throw new Error(res.data.message)
  },
}
