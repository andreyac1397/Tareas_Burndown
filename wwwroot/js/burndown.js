document.addEventListener("DOMContentLoaded", () => {
    const lienzo = document.getElementById("graficoBurndown");
    if (!lienzo || !window.datosBurndown) return;

    const etiquetas = window.datosBurndown.map(item => item.Dia);
    const lineaIdeal = window.datosBurndown.map(item => item.Ideal);
    const lineaReal = window.datosBurndown.map(item => item.Real);

    new Chart(lienzo, {
        type: "line",
        data: {
            labels: etiquetas,
            datasets: [
                {
                    label: "Línea ideal",
                    data: lineaIdeal,
                    borderColor: "#16a34a",
                    backgroundColor: "rgba(22, 163, 74, 0.08)",
                    borderDash: [6, 6],
                    borderWidth: 3,
                    pointRadius: 4,
                    pointHoverRadius: 6,
                    tension: 0.3,
                    fill: false
                },
                {
                    label: "Línea real",
                    data: lineaReal,
                    borderColor: "#dc2626",
                    backgroundColor: "rgba(220, 38, 38, 0.08)",
                    borderWidth: 3,
                    pointRadius: 4,
                    pointHoverRadius: 6,
                    tension: 0.3,
                    fill: false
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            interaction: {
                mode: "index",
                intersect: false
            },
            plugins: {
                legend: {
                    position: "bottom",
                    labels: {
                        font: {
                            size: 14
                        },
                        padding: 20
                    }
                },
                title: {
                    display: true,
                    text: "Burndown Chart del Sprint",
                    font: {
                        size: 18
                    },
                    padding: {
                        top: 10,
                        bottom: 20
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        stepSize: 1,
                        font: {
                            size: 13
                        }
                    },
                    title: {
                        display: true,
                        text: "Puntos restantes",
                        font: {
                            size: 14,
                            weight: "bold"
                        }
                    },
                    grid: {
                        drawBorder: true
                    }
                },
                x: {
                    ticks: {
                        font: {
                            size: 13
                        }
                    },
                    title: {
                        display: true,
                        text: "Días del sprint",
                        font: {
                            size: 14,
                            weight: "bold"
                        }
                    },
                    grid: {
                        display: false
                    }
                }
            }
        }
    });
});