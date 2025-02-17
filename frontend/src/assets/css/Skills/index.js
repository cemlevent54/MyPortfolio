document.addEventListener("DOMContentLoaded", () => {
    const filterBtns = document.querySelectorAll(".filter-btn");
    const skillCards = document.querySelectorAll(".skill-card");
  
    filterBtns.forEach((btn) => {
      btn.addEventListener("click", () => {
        const category = btn.getAttribute("data-category");
  
        // Toggle active button
        filterBtns.forEach((b) => b.classList.remove("active"));
        btn.classList.add("active");
  
        // Show/hide cards
        skillCards.forEach((card) => {
          const cardCategory = card.getAttribute("data-category");
          if (category === "all" || cardCategory === category) {
            card.style.display = "block";
          } else {
            card.style.display = "none";
          }
        });
      });
    });
  
    // Initialize by showing all
    document.querySelector(".filter-btn[data-category='all']").click();
  });
  