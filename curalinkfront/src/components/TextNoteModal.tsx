import { useState } from "react";
import axios from "axios";

export default function TextNoteModal({ onClose }: { onClose: () => void }) {
    const [text, setText] = useState("");

    const submit = async () => {
        await axios.post("https://localhost:5001/api/reports", {
            staffId: 1,
            residentId: 1,
            content: text,
        });
        onClose();
    };

    return (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
            <div className="bg-white p-6 rounded-xl w-96">
                <h2 className="text-lg font-semibold mb-4">Text Note</h2>
                <textarea
                    className="w-full border rounded p-2"
                    rows={5}
                    value={text}
                    onChange={(e) => setText(e.target.value)}
                />
                <div className="flex justify-end mt-4 space-x-2">
                    <button onClick={onClose} className="px-4 py-2 bg-gray-200 rounded">
                        Cancel
                    </button>
                    <button
                        onClick={submit}
                        className="px-4 py-2 bg-green-500 text-white rounded"
                    >
                        Save
                    </button>
                </div>
            </div>
        </div>
    );
}
