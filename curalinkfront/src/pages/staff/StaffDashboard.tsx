/* eslint-disable @typescript-eslint/no-explicit-any */
import api from "@/api/axiosConfig";
import { useRef, useState } from "react";
import { useNavigate } from "react-router-dom";

interface ReportRequest {
    residentId: number;
    staffId: number;
    textReport: string;
}

export default function StaffDashboard() {
    const navigate = useNavigate();
    const [showTextModal, setShowTextModal] = useState(false);
    const [reportText, setReportText] = useState("");
    const [residentId] = useState<number>(1);
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
        } catch (error: any) {
            console.error("Error processing report:", error);
            alert(`Fehler: ${error.response?.data || error.message}`);
        } finally {
            setLoading(false);
        }
    };

    const goToResident = () => {
        navigate(`/resident/${residentId}/overview`);
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
        <div className="StaffWrap">
            <div className="Wrapper">
                <div className="flex gap-4 items-center">
                    <button
                        onClick={goToResident}
                        className="ResOverview"
                        style={{
                        }}
                    >
                        Zu Bewohner Uebersicht →
                    </button>
                </div>

                <div className="BtnWrapper">
                    <div className="CentrWrap">
                        <div className="FlexWrap">
                            {!isRecording ? (
                                <button
                                    className="VoiceBtn"
                                    onClick={startRecording}
                                >
                                    Voice Note
                                </button>
                            ) : (
                                <button
                                    className="VoiceBtn"
                                    style={{ backgroundColor: "#008E56" }}
                                    onClick={stopRecording}
                                >
                                    Stop Recording
                                </button>
                            )}
                            <button
                                onClick={() => setShowTextModal(true)}
                                className="border px-4 py-2 rounded-lg hover:bg-gray-100 TextBtn"
                            >
                                Text Note
                            </button>
                        </div>
                    </div>

                    <section>
                        <h2>
                            Offene Aufgaben
                        </h2>
                        <p className="toDo" >
                            Keine offene Aufgabe
                        </p>
                    </section>

                    <section>
                        <h2>
                            Letzte Aktivitaeten
                        </h2>
                        <ul>
                            <li>Neuer Bericht erstellt (ID #123)</li>
                        </ul>
                    </section>

                    <p className="Slogan">
                        Care that connects.
                    </p>
                </div>
            </div>

            <aside className=" Tagesplan">
                <h3>
                    Tagesplan
                </h3>
                <ul>
                    <li>
                        <p className="font-medium" style={{ color: "black" }}>
                            Max Mustermann
                        </p>
                        <p className="text-sm text-gray-600" style={{ color: "black" }}>
                            Zimmer 12A - Fruehstueck 08:00
                        </p>
                    </li>
                </ul>
            </aside>

            {showTextModal && (
                <div className="PopupWrap">
                    <div className="bg-white p-6 rounded-xl w-96 PopupDiv" >
                        <h3 className="text-lg font-semibold mb-4" style={{ color: "black" }}>
                            Text Note eingeben
                        </h3>
                        <textarea
                            placeholder="Bericht eingeben..."
                            value={reportText}
                            onChange={(e) => {
                                setReportText(e.target.value);
                                e.target.style.height = 'auto';
                                e.target.style.height = `${e.target.scrollHeight}px`
                            }}
                        ></textarea>
                        <div className="PopupBtnWrap">
                            <button
                                onClick={() => {
                                    setShowTextModal(false);
                                    setReportText("");
                                }}
                                className="TextBtn"
                                disabled={loading}
                            >
                                Abbrechen
                            </button>
                            <button
                                onClick={handleSubmitReport}
                                className="VoiceBtn"
                                disabled={loading}
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