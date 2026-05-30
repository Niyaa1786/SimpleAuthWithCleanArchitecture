<script setup lang="ts">
import { reactive } from 'vue'
import { useLoginForm } from '@/composables/useLoginForm'
import type { LoginRequest } from '@/types/auth'

const state = reactive<LoginRequest>({
  username: '',
  email: '',
  password: '',
})

const { isLoading, onSubmit, loginSchema } = useLoginForm()

const handleSubmit = () => {
  onSubmit(state)
}
</script>

<template>
  <UContainer class="min-h-screen flex items-center justify-center">
    <UCard class="w-full max-w-sm">
      <template #header>
        <h2 class="text-2xl font-bold text-center">Đăng nhập</h2>
      </template>
      <UForm
        :state="state"
        :schema="loginSchema"
        class="space-y-4 min-w-ful"
        @submit="handleSubmit"
      >
        <UFormField label="Tên đăng nhập" name="username">
          <UInput v-model="state.username" placeholder="Tên đăng nhập" class="w-full" />
        </UFormField>
        <UFormField label="Email" name="email">
          <UInput v-model="state.email" placeholder="Email" type="email" class="w-full" />
        </UFormField>
        <UFormField label="Mật khẩu" name="password">
          <UInput v-model="state.password" type="password" placeholder="Mật khẩu" class="w-full" />
        </UFormField>
        <UButton type="submit" :loading="isLoading" block>Đăng nhập</UButton>
      </UForm>
      <template #footer>
        <p class="text-center text-sm">
          Chưa có tài khoản?
          <RouterLink to="/register" class="text-primary-500 hover:underline">Đăng ký</RouterLink>
        </p>
      </template>
    </UCard>
  </UContainer>
</template>
