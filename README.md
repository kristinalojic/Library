# Library

## Opis

Library je sveobuhvatna aplikacija koja olakšava vođenje evidencije i upravljanje bibliotekarskim poslovima, omogućavajući efikasnu organizaciju i pružanje usluga korisnicima. Aplikacija podržava dva tipa korisničkih naloga: administrator i bibliotekar, pri čemu svaki tip naloga ima različite dozvole i funkcionalnosti.

## Prijava

Prilikom pokretanja aplikacije, korisniku se prikazuje početni ekran za prijavu. Proces prijave je isti za sve tipove korisnika.

![Image](https://github.com/user-attachments/assets/ce413be1-f190-4447-ba68-ee215dd1d6d8)

Korisnik ima mogućnost izbora jezika putem opcije smještene u donjem desnom uglu ekrana.

Da bi pristupio aplikaciji, korisnik mora unijeti ispravne kredencijale – korisničko ime i lozinku u za to predviđena polja, a zatim kliknuti na dugme "Login" ili pritisnuti taster "Enter". Ako su kredencijali ispravni, korisniku se prikazuje glavni prozor sa funkcionalnostima prilagođenim njegovom tipu naloga.

U slučaju pogrešnog unosa ili pokušaja prijave bez unosa podataka, prikazuje se odgovarajuća poruka o neuspješnoj prijavi.

![Image](https://github.com/user-attachments/assets/244b916f-a4c0-459e-af5e-b85f55d5ae44)

## Administrator

Administratorski dio aplikacije omogućava upravljanje zaposlenima i knjigama, kao i podešavanje opštih postavki aplikacije. Takođe, administrator ima mogućnost odjave iz aplikacije.

### Rad sa zaposlenima

Izborom stavke "Zaposleni" iz menija administratoru se prikazuje prozor sa dva taba – jedan za aktivne bibliotekare, a drugi za neaktivne.

#### Trenutno aktivni zaposleni

U sredini ekrana prikazuje se tabela sa osnovnim informacijama o aktivnim zaposlenima. Iznad tabele se nalaze tri dugmeta: "Dodaj", "Ukloni" i "Detalji".

![Image](https://github.com/user-attachments/assets/061e8f38-ff6d-4df0-a683-b7e9f7fe279f)

Klikom na dugme **Dodaj** otvara se formu za unos novog zaposlenog. 

![Image](https://github.com/user-attachments/assets/bc5c3c50-aea6-4acd-8534-dc1f3c6d341d)

Prilikom unosa provjerava se da li već postoji zaposleni sa istim korisničkim imenom, JMBG-om, email adresom ili brojem telefona. U slučaju poklapanja, prikazuje se upozorenje.  Dugme **"Otkaži"** prekida unos novog zaposlenog.

![Image](https://github.com/user-attachments/assets/bbba63b6-80bf-4f59-a009-d10b9bd79906)

Dugme **Ukloni** deaktivira selektovanog zaposlenog i prebacuje ga među neaktivne. Ako nije selektovan nijedan zaposleni, prikazuje se upozorenje.

![Image](https://github.com/user-attachments/assets/46605fa9-e55a-4d15-9ef0-ea6307dc56a3)

Dugme **Ažuriraj** otvara formu za ažuriranje podataka o selektovanom zaposlenom. Polja su popunjena postojećim informacijama i mogu se mijenjati uz provjeru duplikata. Ako nije selektovan zaposleni, prikazuje se upozorenje.

![Image](https://github.com/user-attachments/assets/cb29d81f-c06c-4900-a7ed-f083b7c1ffe1)

Iznad tabele nalazi se polje za pretragu po imenu i prezimenu.

![Image](https://github.com/user-attachments/assets/da53b005-a5d4-4f49-a98a-c5a888f9ce34)

#### Neaktivni zaposleni

U ovom tabu prikazuje se tabela sa osnovnim informacijama o neaktivnim zaposlenima. 

![Image](https://github.com/user-attachments/assets/4d5fad72-a66b-41c3-bb6a-0c05dcea5327)

Iznad tabele nalazi se dugme **"Aktiviraj"**.Prije aktivacije neophodno je selektovati zaposlenog. Ako nije selektovan nijedan, prikazuje se upozorenje. Klikom na **"Aktiviraj"**, zaposleni se ponovo aktivira i prebacuje u tabelu aktivnih zaposlenih.

Pretraga je moguća po imenu i prezimenu.

### Rad sa knjigama

Izborom opcije "Knjige" iz menija administratoru se prikazuju dva taba – za dostupne i arhivirane knjige.

#### Dostupne knjige

Tabela prikazuje informacije o trenutno dostupnim knjigama. Iznad nje se nalaze dugmad: **"Dodaj"**, **"Ukloni"** i **"Ažuriraj"**.

![Image](https://github.com/user-attachments/assets/af4810bb-b13c-4ad5-afe3-315a178890ea)

**Dodaj** otvara formu za unos nove knjige. Sistem provjerava da li već postoji knjiga sa istim nazivom i autorom; upozorenje se prikazuje u slučaju poklapanja. **"Otkaži"** prekida unos.

  ![Image](https://github.com/user-attachments/assets/ff4635b1-a3b1-49b1-82f8-f3469f963f0c)
  
**Ukloni** arhivira selektovanu knjigu. Ukoliko nije selektovana nijedna knjiga, prikazuje se upozorenje.

![Image](https://github.com/user-attachments/assets/6b1b8fcd-0ca3-4cb8-abf1-1be4f773b8ab)

**Ažuriraj** otvara formu za ažuriranje podataka o selektovanoj knjizi. Ukoliko knjiga nije selektovana prikazuje se upozorenje.

![Image](https://github.com/user-attachments/assets/d4a8fc47-6663-4526-8de1-a87a5d194a4b)

Iznad tabele nalazi se polje za pretragu po nazivu i autoru.

![Image](https://github.com/user-attachments/assets/cbd5667d-e1e3-4143-aa31-ed2f185ce0b1)

#### Arhivirane knjige

Tabela prikazuje arhivirane knjige, a iznad nje se nalazi dugme **"Aktiviraj"**.

![image](https://github.com/user-attachments/assets/24827fcc-d301-43a5-873a-3ed7a37af9be)

Da bi knjiga bila aktivirana, potrebno je prethodno selektovati željeni red. Klikom na **"Aktiviraj"**, knjiga se vraća među dostupne za izdavanje.

Pretraga je dostupna po nazivu i autoru.

### Opšta podešavanja

Administrator može podešavati:

- **Temu**: svijetla ili tamna.
- **Boju**: koristi se za elemente poput menija i dugmadi.
- **Jezik**: srpski ili engleski.

  ![Image](https://github.com/user-attachments/assets/59fd2224-4da5-4381-8387-d7d1611c33a4)

Izabrane postavke se čuvaju i primjenjuju pri sljedećoj prijavi korisnika.

## Bibliotekar

Bibliotekarski deo aplikacije omogućava:
- upravljanje članovima biblioteke,
- zaduživanje i razduživanje knjiga,
- promenu opštih podešavanja,
- odjavu iz sistema.

### Rad sa članovima biblioteke

Izborom opcije "Članovi" iz menija bibliotekaru se prikazuje tabela sa osnovnim informacijama o članovima. Iznad tabele su dostupna dugmad **"Dodaj"** i **"Detalji"**.

![Image](https://github.com/user-attachments/assets/6ba38ef5-3716-4864-8f15-4b777c3d836e)

Dugme **Dodaj** otvara formu za unos novog člana. Sistem proverava jedinstvenost **email adrese** i **broja telefona**, u slučaju poklapanja, prikazuje se upozorenje. Klik na **"Otkaži"** prekida unos.

![Image](https://github.com/user-attachments/assets/40fb944c-7214-4004-9047-bf9613c2797a)

Dugme **Detalji** otvara prozor sa informacijama o selektovanom članu — aktivna zaduženja, istorija zaduživanja i polja za ažuriranje podataka. Prije klika na dugme neophodno je selektovati člana, u suprotnom se prikazuje upozorenje.

#### Trenutna zaduženja

Tabela prikazuje sve aktivne zaduženja člana sa informacijama o roku vraćanja i tome da li je rok vraćanja već bio produžavan.

![Image](https://github.com/user-attachments/assets/cd32dbf4-b750-4f6e-886a-c511ab924511)

Iznad tabele bibliotkar vidi dva dugmeta:
- **Razduži**: otvara potvrdu vraćanja knjige. Klikom na **"Da"**, knjiga se uklanja iz aktivnih zaduženja, broj dostupnih primeraka se ažurira i pamti se u istoriji zaduženja člana.
  
  ![Image](https://github.com/user-attachments/assets/07897f9f-49fc-457f-8406-8243bc83f8b3)
  
- **Produži**: otvara potvrdu produženja roka za 7 dana. Mogućnost produženja je ograničena na jednom i u poslednja tri dana pre isteka.


#### Istorija zaduživanja

Prikaz svih prethodnih zaduživanja člana, uključujući datum zaduživanja i vraćanja.

![Image](https://github.com/user-attachments/assets/bfd3c166-af94-46b4-ae49-0217f879e5a8)

#### Ažuriranje podataka

Otvara se forma sa popunjenim poljima člana. Bibliotekar može menjati podatke uz proveru jedinstvenosti **email adrese** i **broja telefona**. Klikom na **"Ažuriraj"**, izmene se čuvaju.

![Image](https://github.com/user-attachments/assets/f58dc76c-7256-4b86-8d12-87024968ad4e)

Članarinu je moguće produžiti klikom na "+" pored informacija o isteku članarine. Članarina se produžava na godinu dana i to ukoliko nije ostalo više od mjesec dana do isteka važeće članarine ili je članarina već istekla. Klikom na "+" otvara se forma za potvrdu produžavanja clanarine.

![Image](https://github.com/user-attachments/assets/496c8caa-4bad-4192-9368-8efea83035b7)

Klikom na "Da" članarina se produžava i nije neophodno kliknuti na dugme "Ažuriraj" (članarina je već produženja potvrdom na formi).

### Rad sa knjigama

Izborom opcije "Knjige" iz menija bibliotekaru se prikazuje tabela sa dostupnim knjigama i dugmad **"Zaduži"** i **"Vidi zaduženja"**.

![Image](https://github.com/user-attachments/assets/d1384d51-f67d-481a-9d80-ed9025befb6d)

Dugme **Zaduži** otvara formu za zaduživanje. Bibliotekar bira člana sa aktivnom članarinom i unosi rok vraćanja. Sistem onemogućava novo zaduživanje iste knjige dok nije vraćena.

![Image](https://github.com/user-attachments/assets/7e3c4947-6006-4ecd-9e18-dd262c533e8e)

Dugme **Vidi zaduženja** otvara prozor sa detaljima o aktivnim i prošlim zaduživanja za odabranu knjigu.

![Image](https://github.com/user-attachments/assets/dc63a003-5288-405c-89ba-9a2aaf474cea)

![Image](https://github.com/user-attachments/assets/38bdd930-53e9-4d61-91f3-f10962ef4e30)

### Opšta podešavanja

Bibliotekaru su dostupne iste opcije kao administratoru:
- **Tema**: svetla ili tamna,
- **Akcentna boja**: za meni, dugmad i istaknute elemente,
- **Jezik**: srpski ili engleski.

Izabrana podešavanja se pamte i primenjuju pri narednom logovanju.


