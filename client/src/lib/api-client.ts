import { env } from "@/config/env";
import { getAccessToken } from "@/features/users/store/session-store";
import Axios, { type InternalAxiosRequestConfig } from "axios";
import { toast } from "sonner";

function authRequestInterceptor(config: InternalAxiosRequestConfig) {
  if (config.headers) {
    config.headers.Accept = "application/json";
  }

  const accessToken = getAccessToken();

  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`;
  }

  config.withCredentials = true;
  return config;
}

export const api = Axios.create({
  baseURL: env.API_URL,
});

api.interceptors.request.use(authRequestInterceptor);

api.interceptors.response.use(
  (response) => {
    return response.data;
  },
  (error) => {
    toast.error("Произошла ошибка", {
      description: error.response.data.detail,
    });

    console.log("ApiClientError:", error);
    return Promise.reject(error);
  }
);
