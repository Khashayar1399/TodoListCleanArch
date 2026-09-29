import axios from 'axios';

export interface TodoItem {
  id: string;
  title: string;
  description?: string;
  isCompleted: boolean;
  createdAt: string;
  dueDate?: string | null;
}

export interface CreateTodoRequest {
  title: string;
  description?: string;
  dueDate?: string | null;
}

export interface UpdateTodoRequest {
  title: string;
  description?: string;
  dueDate?: string | null;
}

// پورت سرور دات‌نت شما (همان پورتی که با F5 باز می‌شود)
const API_BASE_URL = 'https://localhost:7036/api/Todos';

export const todoApi = {
  getAll: async (): Promise<TodoItem[]> => {
    const res = await axios.get<TodoItem[]>(API_BASE_URL);
    return res.data;
  },

  create: async (data: CreateTodoRequest): Promise<string> => {
    const res = await axios.post<string>(API_BASE_URL, data);
    return res.data;
  },

  update: async (id: string, data: UpdateTodoRequest): Promise<void> => {
    await axios.put(`${API_BASE_URL}/${id}`, data);
  },

  toggle: async (id: string): Promise<void> => {
    await axios.patch(`${API_BASE_URL}/${id}/toggle`);
  },

  delete: async (id: string): Promise<void> => {
    await axios.delete(`${API_BASE_URL}/${id}`);
  }
};