import { useState } from "react";

export default function AssistantPage() {
    const [note, setNote] = useState("");

    const handleSubmit = () => {
        console.log("Отправлен отчёт:", note);
        // TODO:  axios.post for backend (LLMController)
    };

    return (
        <div className="min-h-screen bg-gray-50 flex items-center justify-center p-6">
            <div className="bg-white shadow-lg rounded-2xl p-8 w-full max-w-lg">
                <h1 className="text-2xl font-bold mb-6 text-center">Assistant Dashboard</h1>

                <textarea
                    className="w-full border rounded-xl p-3 focus:ring-2 focus:ring-blue-500 mb-4"
                    rows={6}
                    placeholder="Введите текстовый отчёт..."
                    value={note}
                    onChange={(e) => setNote(e.target.value)}
                />

                <button
                    onClick={handleSubmit}
                    className="w-full bg-blue-600 text-white py-3 rounded-xl shadow hover:bg-blue-700 transition"
                >
                    Text Note
                </button>
            </div>
        </div>
    );
}