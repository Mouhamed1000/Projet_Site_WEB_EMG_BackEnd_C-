# Projet_Site_WEB_EMG_BackEnd_C# Par Med1000  
- Ceci est un backend C# pour le projet de site Emg
# Pour cloner le projet faites : git clone https://github.com/Mouhamed1000/Projet_Site_WEB_EMG_BackEnd_C-.git  
- Une fois après avoir cloné le projet, faites sur votre terminal : cd Projet_Site_WEB_EMG_BackEnd_C  
# Une fois que vous avez ouvert le projet avec votre éditeur, faites : dotnet restore pour voir si vous avez les dépendances nécessaires  
# Avec d'appliquer les migrations, vérifiez le fichier appsettings.json puis dans la section VoitureDb et IdentityDb, remplacez votre login et mot de passe  
# Si vous voulez utilisez les mêmes utilisateurs, le nom d'utilisateur à créer est : UserEMG et le password est : passer  
# Les bases de données que j'ai utilisé : projetEMGCarsDb et projetEMGIdentitydb  
# Si c'est bon, faites : dotnet ef database update --context VoitureContext  
# Puis faites : dotnet ef database update --context ApplicationDbContext
# Ensuite, faites dotnet run  
# Le port que j'ai configuré est 5000 , assurez-vous que ce port soit libre  
# Dans le cas contraire, essayer de le liberer, soit en redémarrant la machine ou en faisant kill du processus qui l'utilise  




