export interface ApiResponse<T> {
  isSuccess: boolean
  message: string
  data: T
  errors: unknown
  timeStamp: string
}
