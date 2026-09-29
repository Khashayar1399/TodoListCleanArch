import React, { useEffect, useState } from 'react';
import { todoApi, type TodoItem } from './api';
import './App.css';

export function App() {
  const [todos, setTodos] = useState<TodoItem[]>([]);
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [dueDate, setDueDate] = useState('');
  const [editingId, setEditingId] = useState<string | null>(null);
  const [filter, setFilter] = useState<'all' | 'pending' | 'completed'>('all');
  const [loading, setLoading] = useState(false);

  const fetchTodos = async () => {
    try {
      setLoading(true);
      const data = await todoApi.getAll();
      setTodos(data);
    } catch {
      alert('خطا در برقراری ارتباط با سرویس دات‌نت.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTodos();
  }, []);

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!title.trim()) return;

    const payloadDueDate = dueDate ? new Date(dueDate).toISOString() : null;

    try {
      if (editingId) {
        await todoApi.update(editingId, { title, description, dueDate: payloadDueDate });
        setEditingId(null);
      } else {
        await todoApi.create({ title, description, dueDate: payloadDueDate });
      }

      setTitle('');
      setDescription('');
      setDueDate('');
      await fetchTodos();
    } catch (err: any) {
      alert('خطا در ذخیره‌سازی: ' + (err.response?.data?.message || err.message));
    }
  };

  const startEdit = (item: TodoItem) => {
    setEditingId(item.id);
    setTitle(item.title);
    setDescription(item.description || '');
    setDueDate(item.dueDate ? item.dueDate.split('T')[0] : '');
  };

  const cancelEdit = () => {
    setEditingId(null);
    setTitle('');
    setDescription('');
    setDueDate('');
  };

  const handleToggle = async (id: string) => {
    try {
      await todoApi.toggle(id);
      await fetchTodos();
    } catch (err: any) {
      alert('خطا در تغییر وضعیت: ' + (err.response?.data?.message || err.message));
    }
  };

  const handleDelete = async (id: string) => {
    if (!window.confirm('آیا از حذف این تسک اطمینان دارید؟')) return;
    try {
      await todoApi.delete(id);
      await fetchTodos();
    } catch (err: any) {
      alert('خطا در حذف: ' + (err.response?.data?.message || err.message));
    }
  };

  // محاسبات آمار
  const totalCount = todos.length;
  const completedCount = todos.filter((t) => t.isCompleted).length;
  const pendingCount = totalCount - completedCount;
  const progressPercent = totalCount > 0 ? Math.round((completedCount / totalCount) * 100) : 0;

  const filteredTodos = todos.filter((t) => {
    if (filter === 'completed') return t.isCompleted;
    if (filter === 'pending') return !t.isCompleted;
    return true;
  });

  return (
    <div className="dashboard-wrapper">
      {/* هدر */}
      <header className="app-header">
        <h1>مدیریت وظایف کاری</h1>
        <p>.NET 8 Clean Architecture (CQRS) + React & Vite</p>
      </header>

      {/* خلاصه وضعیت */}
      <div className="stats-grid">
        <div className="stat-card">
          <span>کل تسک‌ها</span>
          <strong>{totalCount}</strong>
        </div>
        <div className="stat-card">
          <span>تکمیل‌شده</span>
          <strong style={{ color: '#86efac' }}>{completedCount}</strong>
        </div>
        <div className="stat-card">
          <span>درصد پیشرفت</span>
          <strong style={{ color: '#93c5fd' }}>%{progressPercent}</strong>
        </div>
      </div>

      {/* فرم ثبت یا ویرایش */}
      <div className="main-card">
        <form onSubmit={handleSave} className="form-group">
          <div className="row-inputs">
            <input
              type="text"
              placeholder="عنوان وظیفه جدید..."
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              required
            />
            <input
              type="date"
              value={dueDate}
              onChange={(e) => setDueDate(e.target.value)}
              title="تاریخ سررسید"
            />
          </div>
          <textarea
            rows={2}
            placeholder="توضیحات و جزییات تسک (اختیاری)..."
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <div className="btn-container">
            <button type="submit" className="btn btn-primary">
              {editingId ? 'بروزرسانی تسک' : 'افزودن به لیست'}
            </button>
            {editingId && (
              <button type="button" onClick={cancelEdit} className="btn btn-cancel">
                انصراف
              </button>
            )}
          </div>
        </form>
      </div>

      {/* نوار فیلترها */}
      <div className="filter-bar">
        <div className="filter-group">
          <button
            className={`filter-btn ${filter === 'all' ? 'active' : ''}`}
            onClick={() => setFilter('all')}
          >
            همه ({totalCount})
          </button>
          <button
            className={`filter-btn ${filter === 'pending' ? 'active' : ''}`}
            onClick={() => setFilter('pending')}
          >
            در حال انجام ({pendingCount})
          </button>
          <button
            className={`filter-btn ${filter === 'completed' ? 'active' : ''}`}
            onClick={() => setFilter('completed')}
          >
            پایان‌یافته ({completedCount})
          </button>
        </div>
        {loading && <span style={{ color: '#94a3b8', fontSize: '0.85rem' }}>در حال همگام‌سازی...</span>}
      </div>

      {/* لیست تسک‌ها */}
      <div className="todo-list">
        {filteredTodos.length === 0 && !loading && (
          <div className="empty-state">
            <p style={{ margin: 0, fontSize: '1.1rem' }}>تسکی در این بخش وجود ندارد.</p>
          </div>
        )}

        {filteredTodos.map((item) => (
          <div key={item.id} className={`todo-card ${item.isCompleted ? 'completed' : ''}`}>
            <div className="todo-main">
              <input
                type="checkbox"
                className="custom-checkbox"
                checked={item.isCompleted}
                onChange={() => handleToggle(item.id)}
              />
              <div>
                <div className="todo-title">{item.title}</div>
                {item.description && <p className="todo-desc">{item.description}</p>}
                <div className="meta-tags">
                  <span className="tag">
                    ایجاد: {new Date(item.createdAt).toLocaleDateString('fa-IR')}
                  </span>
                  {item.dueDate && (
                    <span className="tag due">
                      موعد: {new Date(item.dueDate).toLocaleDateString('fa-IR')}
                    </span>
                  )}
                </div>
              </div>
            </div>

            <div className="actions-group">
              <button
                className="action-btn edit"
                onClick={() => startEdit(item)}
                title="ویرایش"
              >
                ✏️
              </button>
              <button
                className="action-btn delete"
                onClick={() => handleDelete(item.id)}
                title="حذف"
              >
                🗑️
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default App;