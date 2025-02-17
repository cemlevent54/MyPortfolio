<script setup>
import { ref } from "vue";
import axios from "axios";

// Form alanlarını tanımlıyoruz
const name = ref("");
const email = ref("");
const subject = ref("");
const message = ref("");
const successMessage = ref("");
const errorMessage = ref("");

// Mail gönderme fonksiyonu
const sendEmail = async () => {
  successMessage.value = "";
  errorMessage.value = "";

  try {
    console.log(name.value, email.value, subject.value, message.value);
    const response = await axios.post("http://localhost:5000/api/emailsend/send", {
      name: name.value,
      email: email.value,
      subject: subject.value,
      message: message.value,
    });

    if (response.status === 200) {
      successMessage.value = "Your message has been sent successfully!";
      errorMessage.value = "";
      // Form alanlarını temizle
      name.value = "";
      email.value = "";
      subject.value = "";
      message.value = "";
      alert("Your message has been sent successfully! Thank you for contacting me.");
    } else {
      throw new Error("Failed to send email");
    }
  } catch (error) {
    errorMessage.value = "Error sending email. Please try again later.";
  }
};
</script>

<template>
  <section id="contact" class="contact section">
    <div class="container section-title" data-aos="fade-up">
      <h2>Contact</h2>
      <p>Feel free to reach out to us by filling the form below.</p>
    </div>

    <div class="container" data-aos="fade-up" data-aos-delay="100">
      <div class="row gy-4">
        <div class="col-lg-12">
          <form @submit.prevent="sendEmail" class="php-email-form" data-aos="fade-up" data-aos-delay="200">
            <div class="row gy-4">
              <div class="col-md-6">
                <label for="name-field" class="pb-2">Your Name</label>
                <input type="text" v-model="name" id="name-field" class="form-control" required />
              </div>

              <div class="col-md-6">
                <label for="email-field" class="pb-2">Your Email</label>
                <input type="email" v-model="email" id="email-field" class="form-control" required />
              </div>

              <div class="col-md-12">
                <label for="subject-field" class="pb-2">Subject</label>
                <input type="text" v-model="subject" id="subject-field" class="form-control" required />
              </div>

              <div class="col-md-12">
                <label for="message-field" class="pb-2">Message</label>
                <textarea v-model="message" id="message-field" class="form-control" rows="5" required></textarea>
              </div>

              <div class="col-md-12 text-center">
                <button type="submit">Send Message</button>
              </div>
              
              <!-- Başarı ve hata mesajları -->
              <div v-if="successMessage" class="sent-message text-success">{{ successMessage }}</div>
              <div v-if="errorMessage" class="error-message text-danger">{{ errorMessage }}</div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>
