<template>
  <div class="basement">
    <div class="all">
      <div class="contact_list">
        <div class="contact">
          <div class="phone">Телефон</div>
          <a href="tel:89139409888" class="number">8 913 940 98 88</a>
        </div>
      </div>
      <div class="copyright">
        <div class="social_media">
          <ul class="links">
            <li class="icons">
              <a class="link" href="https://t.me/krona_businessclub">
                <font-awesome-icon :icon="['fab', 'telegram']" />
              </a>
            </li>
            <li class="icons">
              <a class="link" href="https://vk.com/krona_club_nsk">
                <font-awesome-icon :icon="['fab', 'vk']" />
              </a>
            </li>
            <li class="icons">
              <a class="link" href="https://wa.me/79139409888">
                <font-awesome-icon :icon="['fab', 'whatsapp']" />
              </a>
            </li>
            <li class="icons">
              <a class="link" href="https://go.2gis.com/4x5en">
                <font-awesome-icon :icon="['fas', 'location-dot']" />
              </a>
            </li>
          </ul>
        </div>
        <div class="info">
          <div class="club">Бизнес-клуб Крона</div>
          <div class="krona">©&nbsp;КРОНА 2024</div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="css">
.basement {
  position: absolute;
  bottom: 0%;
  left: 0%;
  height: 150px;
  width: 100%;
  display: flex;
  flex-direction: column;
  background-color: #0F1C2E;
}
.all {
  margin: 15px;
}

.contact_list {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.contact {
  display: flex;
  flex-direction: column;
}

.phone {
  display: flex;
  justify-content: left;
  font-size: 14px;
}

.number {
  font-size: 30px;
  color: white;
  text-decoration: none;
}

.links {
  width: 100%;
  height: 100%;
  list-style: none;
  display: flex;
  justify-content: space-between;
  padding: 0;
  margin: 0;
}

.link {
  color:white;
}

.social_media {
  min-width: 200px;
  display: flex;
  justify-content: space-between;
}

.copyright {
  display: flex;
  justify-content: space-between;
  font-size: 16px;
}

.icons {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 30px;
  color: white;
}

.info {
  display: flex;
  flex-direction: column;
}

.krona {
  display: flex;
  justify-content: right;
}
.club {
  display: flex;
  justify-content: right;
}
</style>