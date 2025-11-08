import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Layout from "./components/Layout";
import "@/styles/style.css";

import ResidentOverview from "./pages/residents/ResidentOverview";
import StaffDashboard from "./pages/staff/StaffDashboard";
import ResidentAusscheidungPage from "./pages/residents/ResidentAusscheidungPage";
import ResidentNutritionPage from "./pages/residents/ResidentNutritionPage";
import ResidentMovementPage from "./pages/residents/ResidentMovementPage";
import ResidentAppointmentsPage from "./pages/residents/ResidentAppointments";
 
function App() {
    const defaultResidentId = 1;

    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Navigate to={`/resident/${defaultResidentId}/overview`} replace />} />

                    <Route path="/resident/:residentId/overview" element={<ResidentOverview />} />
                    <Route path="/resident/:residentId/movement" element={<ResidentMovementPage />} />
                    <Route path="/resident/:residentId/ausscheidung" element={<ResidentAusscheidungPage />} />
                    <Route path="/resident/:residentId/nutrition" element={<ResidentNutritionPage />} />
                    <Route path="/resident/:residentId/appointments" element={<ResidentAppointmentsPage />} />
                    <Route path="staff/dashboard" element={<StaffDashboard />} />

                    {/*<Route path="resident/:id/reports" element={<ResidentReportsPage />} />
                    <Route path="resident/:id/medications" element={<ResidentMedicationsPage />} />
                    <Route path="resident/:id/vitals" element={<ResidentVitalSignsPage />} />
                    <Route path="resident/:id/pain" element={<ResidentPainPage />} />
                    <Route path="resident/:id/documents" element={<ResidentDocumentsPage />} /> */}
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;
