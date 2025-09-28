import { useState } from "react";

export default function AssistantPage() {
    const [note, setNote] = useState("");

    const handleSubmit = () => {
        console.log("Note sent:", note);
        // TODO:  axios.post for backend (LLMController)
    };

    return (
        <div className="min-h-screen bg-gray-50 flex items-center justify-center p-6"
            style={{
                top: "0px",
                left: "0px",
                position:"relative"
            } }>
            <div className="bg-white shadow-lg rounded-2xl p-8 w-full max-w-lg">
                <h1 className="text-2xl font-bold mb-6 text-center">Assistant Dashboard</h1>

                <textarea
                    className="w-full border rounded-xl p-3 focus:ring-2 focus:ring-blue-500 mb-4"
                    rows={6}
                    placeholder="Die Note eingeben..."
                    value={note}
                    onChange={(e) => setNote(e.target.value)}
                />

                <button
                    onClick={handleSubmit}
                    className="w-full bg-blue-600 text-white py-3 rounded-xl shadow hover:bg-blue-700 transition"
                >
                    Submit
                </button>
            </div>
        </div>
    );
}