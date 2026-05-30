<script setup lang="ts">
import { reactive } from 'vue'
import { useRegisterForm } from '@/composables/useRegisterForm'
import type { RegisterRequest } from '@/types/auth'

const state = reactive<RegisterRequest>({
  username: '',
  email: '',
  password: '',
})

const { isLoading, onSubmit, registerSchema } = useRegisterForm()

const handleSubmit = () => {
  onSubmit(state)
}
</script>

<template>
  <UContainer class="min-h-screen flex items-center justify-center">
    <UCard class="w-full max-w-sm">
      <template #header>
        <h2 class="text-2xl font-bold text-center">Đăng ký</h2>
      </template>
      <UForm :state="state" :schema="registerSchema" class="space-y-4" @submit="handleSubmit">
        <UFormField label="Tên đăng nhập" name="username">
          <UInput
            v-model="state.username"
            placeholder="Tên đăng nhập (tối thiểu 6 ký tự)"
            class="w-full"
          />
        </UFormField>
        <UFormField label="Email" name="email">
          <UInput v-model="state.email" type="email" placeholder="Email" class="w-full" />
        </UFormField>
        <UFormField label="Mật khẩu" name="password">
          <UInput
            v-model="state.password"
            type="password"
            placeholder="Mật khẩu (ít nhất 8 ký tự)"
            class="w-full"
          />
        </UFormField>
        <UButton type="submit" :loading="isLoading" block>Đăng ký</UButton>
      </UForm>
      <template #footer>
        <p class="text-center text-sm">
          Đã có tài khoản?
          <RouterLink to="/login" class="text-primary-500 hover:underline">Đăng nhập</RouterLink>
        </p>
      </template>
    </UCard>
  </UContainer>
</template>
