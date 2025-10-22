/* eslint-disable @typescript-eslint/no-explicit-any */
import { useState, useRef } from "react";
import api from "@/api/axiosConfig";

interface ReportRequest {
    residentId: number;
    staffId: number;
    textReport: string;
}

export default function StaffDashboard() {
    const [showTextModal, setShowTextModal] = useState(false);
    const [reportText, setReportText] = useState("");
    const [residentId, setResidentId] = useState<number>(1); // You should get this from somewhere
    const [loading, setLoading] = useState(false);
    const [isRecording, setIsRecording] = useState(false);
    const mediaRecorderRef = useRef<MediaRecorder | null>(null);
    const chunks = useRef<Blob[]>([]);

    const handleSubmitReport = async () => {
        if (!reportText.trim()) {
            alert("Bitte geben Sie einen Bericht ein");
            return;
        }

        setLoading(true);

        try {
            const payload: ReportRequest = {
                residentId: residentId,
                staffId: 1, // TODO: Get actual staff ID from auth/context
                textReport: reportText
            };

            const response = await api.post("/api/LLM/report", payload);

            console.log("Report processed successfully:", response.data);
            alert("Bericht erfolgreich verarbeitet!");

            setShowTextModal(false);
            setReportText("");
        } catch (error:any) {
            console.error("Error processing report:", error);
            alert(`Fehler: ${error.response?.data || error.message}`);
        } finally {
            setLoading(false);
        }
    };

    const startRecording = async () => {
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        const mediaRecorder = new MediaRecorder(stream);

        mediaRecorder.ondataavailable = (e) => {
            if (e.data.size > 0) chunks.current.push(e.data);
        };

        mediaRecorder.onstop = async () => {
            const blob = new Blob(chunks.current, { type: "audio/webm" });
            chunks.current = [];

            const formData = new FormData();
            formData.append("file", blob, "voice_note.webm");

            setLoading(true); 

            try {
                const transcriptionResponse = await api.post("/api/reports/voice", formData, {
                    headers: { "Content-Type": "multipart/form-data" },
                });

                const transcribedText = transcriptionResponse.data;
                console.log("Transcribed Text:", transcribedText);

                if (!transcribedText.trim()) {
                    alert("Leere Sprachnotiz, nichts zu verarbeiten.");
                    setLoading(false);
                    return;
                }

                const payload: ReportRequest = {
                    residentId: residentId,
                    staffId: 1, // TODO: Get actual staff ID
                    textReport: transcribedText
                };

                const analysisResponse = await api.post("/api/LLM/report", payload);

                console.log("Report processed successfully:", analysisResponse.data);
                alert("Sprachnotiz erfolgreich verarbeitet!");

            } catch (err: any) {
                console.error("Fehler bei der Verarbeitung der Sprachnotiz:", err);
                alert(`Fehler: ${err.response?.data || err.message}`);
            } finally {
                setLoading(false);
            }
        };

        mediaRecorder.start();
        mediaRecorderRef.current = mediaRecorder;
        setIsRecording(true);
    };

    const stopRecording = () => {
        mediaRecorderRef.current?.stop();
        setIsRecording(false);
    };

    return (
        <div className="flex flex-1" style={{ display: "inline-flex" }}>
            <div className="flex-1 p-6 space-y-6">
                <div className="flex gap-4 items-center">
                    <label className="text-sm font-medium" style={{ color: "black" }}>
                        Bewohner ID:
                        <input
                            type="number"
                            value={residentId}
                            onChange={(e) => setResidentId(Number(e.target.value))}
                            className="ml-2 px-2 py-1 border rounded"
                            style={{ color: "black" }}
                        />
                    </label>
                </div>

                <div className="flex gap-4">
                    {!isRecording ? (
                        <button
                            className="bg-green-500 text-white px-4 py-2 rounded-lg"
                            onClick={startRecording}
                            style={{ backgroundColor: "#008E56" }}
                        >
                            Voice Note
                        </button>
                    ) : (
                        <button
                            className="bg-red-500 text-white px-4 py-2 rounded-lg"
                            style={{ backgroundColor: "#008E56" }}
                            onClick={stopRecording}
                        >
                            Stop Recording
                        </button>
                    )}
                    <button
                        onClick={() => setShowTextModal(true)}
                        className="bg-white border px-4 py-2 rounded-lg hover:bg-gray-100"
                        style={{
                            color: "#008E56",
                            backgroundColor: "#ffffff",
                            border: "1px solid #EDEDED"
                        }}
                    >
                        Text Note
                    </button>
                </div>

                <section>
                    <h2 className="text-lg font-semibold mb-2" style={{ color: "black" }}>
                        Offene Aufgaben
                    </h2>
                    <p className="text-gray-600" style={{ color: "black" }}>
                        Keine offene Aufgabe
                    </p>
                </section>

                <section>
                    <h2 className="text-lg font-semibold mb-2" style={{ color: "black" }}>
                        Letzte Aktivitaeten
                    </h2>
                    <ul className="text-gray-700 text-sm space-y-1" style={{ color: "black" }}>
                        <li style={{ color: "black" }}>Neuer Bericht erstellt (ID #123)</li>
                    </ul>
                </section>

                <p
                    className="text-green-600 font-bold text-center text-xl mt-10"
                    style={{ color: "#008E56" }}
                >
                    Care that connects.
                </p>
            </div>

            <aside className="w-72 bg-white shadow rounded-xl p-4">
                <h3 className="font-semibold mb-4" style={{ color: "black" }}>
                    Tagesplan
                </h3>
                <ul className="space-y-3">
                    <li>
                        <p className="font-medium" style={{ color: "black" }}>
                            Max Mustermann
                        </p>
                        <p className="text-sm text-gray-600" style={{ color: "black" }}>
                            Zimmer 12A - Fruehstueck 08:00
                        </p>
                    </li>
                    <li>
                        <p className="font-medium" style={{ color: "black" }}>
                            Anna Schmidt
                        </p>
                        <p className="text-sm text-gray-600" style={{ color: "black" }}>
                            Zimmer 7B - Medikament 10:00
                        </p>
                    </li>
                </ul>
            </aside>

            {showTextModal && (
                <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
                    <div className="bg-white p-6 rounded-xl w-96">
                        <h3 className="text-lg font-semibold mb-4" style={{ color: "black" }}>
                            Text Note eingeben
                        </h3>
                        <textarea
                            className="w-full border rounded-lg p-2 h-32 mb-4"
                            placeholder="Bericht eingeben..."
                            value={reportText}
                            onChange={(e) => setReportText(e.target.value)}
                            style={{
                                color: "black",
                                borderColor: "black"
                            }}
                        ></textarea>
                        <div className="flex justify-end gap-3">
                            <button
                                onClick={() => {
                                    setShowTextModal(false);
                                    setReportText("");
                                }}
                                className="px-4 py-2 border rounded-lg"
                                disabled={loading}
                                style={{
                                    color: "black",
                                    backgroundColor: "white"
                                }}
                            >
                                Abbrechen
                            </button>
                            <button
                                onClick={handleSubmitReport}
                                className="px-4 py-2 bg-blue-600 text-white rounded-lg"
                                disabled={loading}
                                style={{
                                    backgroundColor: "#008E56",
                                    color: "white"
                                }}
                            >
                                {loading ? "Verarbeite..." : "Senden"}
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}